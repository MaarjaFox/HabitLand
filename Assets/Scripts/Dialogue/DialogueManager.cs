using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
   private static DialogueManager instance;
   [SerializeField] private TextMeshProUGUI displayNameText;
   [SerializeField] private Animator portraitAnimator;

   [Header("Choices UI")]

   [SerializeField] private GameObject[] choices;

   private TextMeshProUGUI[] choicesText;
   [Header("Audio")]
   [SerializeField] private AudioClip dialogueTypingSoundClip;
   [SerializeField] private AudioMixerGroup audioMixerGroup;
   [SerializeField] private float minPitch = 1.5f;
   [Range(-3, 3)]
   [SerializeField] private float maxPitch = 3f;
   

   private AudioSource audioSource;
   [SerializeField] private bool stopAudioSource;

   private Story currentStory;

   public bool dialogueIsPlaying{ get; private set; }

   private Coroutine displayLineCoroutine;
   
   private const string SPEAKER_TAG = "speaker";

   private const string PORTRAIT_TAG = "portrait";

   private void Awake()
   {
    if (instance != null)
    {
        Debug.LogWarning("Found more than one Dialogue Manager in the scene");
    }
     instance = this;

     audioSource = this.gameObject.AddComponent<AudioSource>();
     audioSource.outputAudioMixerGroup = audioMixerGroup;

   }

   public static DialogueManager GetInstance()
   {
     return instance;
   }

   private void Start()
   {
    dialogueIsPlaying = false;
    dialoguePanel.SetActive(false);

    //get all the choices text
    choicesText = new TextMeshProUGUI[choices.Length];
    int index = 0;
    foreach (GameObject choice in choices)
    {
        choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        index++;
    }
   }

private void Update()
{
    // Return right away if dialogue isn't playing
    if (!dialogueIsPlaying)
    {
        return;
    }

    // Handle continuing to the next line in the dialogue when submitted
    if ((currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Space))
    {
        ContinueStory();
    }
}
   
   public void EnterDialogueMode(TextAsset inkJSON)
   {
    currentStory = new Story(inkJSON.text);
    dialogueIsPlaying = true;
    dialoguePanel.SetActive(true);
    

    ContinueStory();

    
   }

   private IEnumerator ExitDialogueMode()
   {
    yield return new WaitForSeconds(0.2f);

    dialogueIsPlaying = false;
    dialoguePanel.SetActive(false);
    dialogueText.text = "";
   }

   private void ContinueStory()
   {
    if (currentStory.canContinue)
    {
        //set text for the current dialogue line
        if(displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        
        //handle tags
        HandleTags(currentStory.currentTags);
    }
    else
    {
        StartCoroutine(ExitDialogueMode());
    }
   }
   private IEnumerator DisplayLine(string line)
   {
    //empty the dialoguetext
    dialogueText.text = "";
    HideChoices();

    bool isAddingRichTextTag = false;

    //display each letter one at a time
    foreach (char letter in line.ToCharArray())
    {
        //if the submit button is pressed, finish up displaying the line right away
        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            dialogueText.text = line;
            break;
        }
        //check for rich text tag if found, add it without waiting
        if(letter == '<' || isAddingRichTextTag)
        {
            isAddingRichTextTag = true;
            dialogueText.text += letter;
            if(letter == '>')
            {
                isAddingRichTextTag = false;
            }
        }
        //if not rich text, add the next letter and wait a small time
        else
        {
            
            PlayDialogueSound(dialogueText.text.Length);
            dialogueText.text += letter;
            
            
            yield return new WaitForSeconds(typingSpeed);
        }

        
    }
    //display choices, if any, for this dialogue line
    DisplayChoices();

   }
    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % 2 == 0)
        {
             if(stopAudioSource)
            {
                audioSource.Stop();
            }
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(dialogueTypingSoundClip);
        }
    }
   private void HideChoices()
   {
    foreach (GameObject choiceButton in choices)
    {
        choiceButton.SetActive(false);
    }
   }

    private void HandleTags(List<string> currentTags)
    {
        //loop through each tag and handle it accordingly
        foreach(string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                portraitAnimator.Play(tagValue);
                    break;
              default:
                    Debug.LogWarning("Tag came in but it is not currently being handled" + tag);
                    break;

            }
        }
    }
   private void DisplayChoices()
   {
     List<Choice> currentChoices = currentStory.currentChoices;

     //defensive check to make sure our UI can support the number of choices coming in

     if(currentChoices.Count > choices.Length)
     {
        Debug.LogError("More choices were given than UI can support. Number of choices given: "
         + currentChoices.Count);
     }

        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            //any other choices are inactive
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());

   }

   private IEnumerator SelectFirstChoice()
   {
    //Event System requires we clear it first, then wait
    //for at least one frame before we set the current selected object.
    EventSystem.current.SetSelectedGameObject(null);
    yield return new WaitForEndOfFrame();
    EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
   }

   public void MakeChoice(int choiceIndex) //Implemented onclick method
   {
    currentStory.ChooseChoiceIndex(choiceIndex);
    ContinueStory();
   }
}
