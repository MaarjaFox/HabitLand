using UnityEngine;
using UnityEngine.UI;

public class HabitEntryController : MonoBehaviour
{
    public Text habitOutputText;
    public Habit habit;
    public int habitIndex;

    private Toggle[] toggleButtons;

    private void Start()
    {
        toggleButtons = GetComponentsInChildren<Toggle>();

        LoadToggleStates();
        DisplayHabitOutput();
    }

    public void OnToggleChanged()
    {
        SaveToggleStates();
        DisplayHabitOutput();
    }

    private void LoadToggleStates()
    {
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            bool isToggleOn = PlayerPrefs.GetInt("ToggleState_" + habitIndex + "_" + i, 0) == 1;
            toggleButtons[i].isOn = isToggleOn;
        }
    }

    private void SaveToggleStates()
    {
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            bool isToggleOn = toggleButtons[i].isOn;
            PlayerPrefs.SetInt("ToggleState_" + habitIndex + "_" + i, isToggleOn ? 1 : 0);
        }
    }

    public void DisplayHabitOutput()
    {
        habitOutputText.text = habit.habitName;
        Debug.Log("Displaying Habit Output: " + habitOutputText.text);
    }
}