[System.Serializable]
public class Habit
{
    public string habitName;
    // Add more properties if needed

    public Habit() { }

    public Habit(string name)
    {
        habitName = name;
    }
}