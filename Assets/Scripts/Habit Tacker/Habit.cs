using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HabitData
{
    public string Name;
    public bool IsCompleted;
    public int XpPoints;
    public int Coins;
}

public class Habit : MonoBehaviour
{
    public List<HabitData> habits = new List<HabitData>();
}