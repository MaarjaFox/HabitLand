using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HabitManager : MonoBehaviour
{
    public static HabitManager instance;
    public GameObject habitEntryPrefab;
    public GameObject deleteButtonPrefab;
    public Transform calendarPanel;
    public InputField habitInputField;
    public Button addHabitButton;

    private List<Habit> habits;
    private string habitInput;
    private int nextAvailableIndex; // Track the next available index for habits

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        habits = new List<Habit>();
        nextAvailableIndex = 0; // Initialize the next available index to 0
        addHabitButton.onClick.AddListener(AddHabit);
    }

    public void AddHabit()
    {
        string habitName = habitInputField.text;
        if (!string.IsNullOrEmpty(habitName))
        {
            // Check if the habit already exists and is inactive
            int inactiveHabitIndex = habits.FindIndex(h => !h.isActive && h.habitName == habitName);
            if (inactiveHabitIndex != -1)
            {
                habits[inactiveHabitIndex].isActive = true; // Mark the habit as active again
                habitInputField.text = string.Empty;
                return;
            }

            // Check if the maximum habit count has been reached
            if (habits.Count >= 5)
            {
                Debug.Log("Maximum habit count reached. Remove a habit to add a new one.");
                return;
            }

            Habit newHabit = new Habit(habitName);
            newHabit.index = GetNextAvailableIndex(); // Set the index of the new habit
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
        habitEntryPosition.y -= habit.index * habitEntryTransform.rect.height; // Use the habit's index
        habitEntryTransform.anchoredPosition = habitEntryPosition;

        HabitEntryController entryController = habitEntry.GetComponent<HabitEntryController>();
        entryController.habit = habit;
        entryController.habitIndex = habit.index; // Set the correct habit index

        // Create and configure the delete button
        GameObject deleteButton = Instantiate(deleteButtonPrefab, habitEntry.transform);
        Button deleteButtonComponent = deleteButton.GetComponent<Button>();
        deleteButtonComponent.onClick.AddListener(() => DeleteHabit(habitEntry));

        // Make sure the toggleButtons array is initialized
        entryController.toggleButtons = entryController.GetComponentsInChildren<Toggle>();
        habit.entryController = entryController; // Set the entryController property of the Habit class
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

 public void DeleteHabit(GameObject habitEntry)
{
    int habitIndex = habitEntry.GetComponent<HabitEntryController>().habitIndex;
    Habit habit = habits[habitIndex];

    habits.RemoveAt(habitIndex);
    Destroy(habitEntry);

    // Mark the habit as inactive
    habit.isActive = false;

    // Update the indices of the remaining habits (if any)
    for (int i = habitIndex; i < habits.Count; i++)
    {
        // Only update the habit index in the corresponding HabitEntryController
        if (habits[i].entryController != null)
        {
            habits[i].entryController.habitIndex = i;
        }
    }
}

    private int GetNextAvailableIndex()
    {
        // Find the maximum index used by existing habits
        int maxIndex = -1;
        foreach (Habit habit in habits)
        {
            if (habit.index > maxIndex)
            {
                maxIndex = habit.index;
            }
        }

        // Find the next available index
        for (int i = 0; i <= maxIndex + 1; i++)
        {
            if (!habits.Exists(h => h.index == i))
                return i;
        }

        return maxIndex + 1;
    }
}