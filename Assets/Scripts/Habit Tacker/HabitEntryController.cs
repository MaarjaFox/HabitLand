using UnityEngine;
using UnityEngine.UI;

public class HabitEntryController : MonoBehaviour
{
    public Text habitOutputText;
    public Habit habit;
    public int habitIndex;
    public int coinsAmount = 5;
    public int xpValue = 1;

    public Toggle[] toggleButtons;

    private void Start()
    {
        toggleButtons = GetComponentsInChildren<Toggle>();

        // Set the toggles to be invisible initially
        SetTogglesVisibility(false);

        LoadToggleStates();
        DisplayHabitOutput();
        
    }

    public void OnToggleChanged()
    {
        SaveToggleStates();
        DisplayHabitOutput();
        Debug.Log("Grant " + coinsAmount + " coins!");
        
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp ", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);

        GameManager.instance.coins += coinsAmount;
        GameManager.instance.ShowText("+" + coinsAmount + " habit coins!",25,Color.yellow,transform.position,Vector3.up * 25, 3.0f);
    }

    public void DisplayHabitOutput()
    {
        habitOutputText.text = habit.habitName;
        Debug.Log("Displaying Habit Output: " + habitOutputText.text);

        // Set the toggles to be visible
        SetTogglesVisibility(true);
    }

    private void SetTogglesVisibility(bool visible)
    {
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            toggleButtons[i].gameObject.SetActive(visible);
        }
    }

    public void SaveToggleStates()
    {
        foreach (Toggle toggle in toggleButtons)
        {
            PlayerPrefs.SetInt(GetToggleKey(toggle), toggle.isOn ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadToggleStates()
    {
        foreach (Toggle toggle in toggleButtons)
        {
            int toggleState = PlayerPrefs.GetInt(GetToggleKey(toggle), 0);
            toggle.isOn = toggleState == 1;
        }
    }

    private string GetToggleKey(Toggle toggle)
    {
        return "ToggleState_" + habitIndex + "_" + toggle.gameObject.name;
    }

    public void DeleteHabit()
    {
        HabitManager.instance.DeleteHabit(gameObject);
    }
}
