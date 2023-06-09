[System.Serializable]
public class Habit
{
    public string habitName;
    public bool isActive;
    public int index;
    public HabitEntryController entryController; // Add a reference to the corresponding HabitEntryController

    public Habit(string name)
    {
        habitName = name;
        isActive = true;
        index = 0; // Initialize index to -1 by default
    }
}