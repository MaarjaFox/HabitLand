using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HabitManager : MonoBehaviour
{
    public static HabitManager instance;
    public GameObject habitEntryPrefab;
    public Transform calendarPanel;
    public InputField habitInputField;
    public Button addHabitButton;

    private List<Habit> habits;
    private string habitInput;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        habits = new List<Habit>();
        addHabitButton.onClick.AddListener(AddHabit);
    }

    public void AddHabit()
    {
        string habitName = habitInputField.text;
        if (!string.IsNullOrEmpty(habitName))
        {
            Habit newHabit = new Habit(habitName);
            habits.Add(newHabit);
            CreateHabitEntry(newHabit);
            habitInputField.text = string.Empty;
        }
    }

    private void CreateHabitEntry(Habit habit)
{
    GameObject habitEntry = Instantiate(habitEntryPrefab, calendarPanel);
    habitEntry.SetActive(false); // Hide the habitEntry prefab initially

    Text habitNameText = habitEntry.GetComponentInChildren<Text>();
    habitNameText.text = habit.habitName;

    RectTransform habitEntryTransform = habitEntry.GetComponent<RectTransform>();
    Vector2 habitEntryPosition = habitEntryTransform.anchoredPosition;
    habitEntryPosition.y -= (habits.Count - 1) * habitEntryTransform.rect.height;
    habitEntryTransform.anchoredPosition = habitEntryPosition;

    HabitEntryController entryController = habitEntry.GetComponent<HabitEntryController>();
    entryController.habit = habit;
    entryController.habitIndex = habits.Count - 1; // Set the correct habit index

    // Make sure the toggleButtons array is initialized
    entryController.toggleButtons = entryController.GetComponentsInChildren<Toggle>();

    entryController.DisplayHabitOutput();

    SetToggleVisibility(habitEntry, true); // Show the toggles for the newly added habit

    habitEntry.SetActive(true); // Show the habitEntry prefab
}

    private void SetToggleVisibility(GameObject habitEntry, bool visible)
    {
        Toggle[] dayToggles = habitEntry.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in dayToggles)
        {
            toggle.gameObject.SetActive(visible);
        }
    }

    public void SetHabitInput(string input)
    {
        habitInput = input;
    }

    public string GetHabitInput()
    {
        return habitInput;
    }
}