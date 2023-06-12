using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerAuto : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying &&
            collider.gameObject.CompareTag("Fighter"))
        {
            playerInRange = false; // Set playerInRange to false to prevent triggering again until dialogue is finished
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fighter"))
        {
            playerInRange = true; // Set playerInRange back to true when the player exits the trigger
        }
    }

    // Call this method to allow triggering the dialogue again
    public void ResetTrigger()
    {
        playerInRange = true;
    }
}

