using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HabitTrackerUI : MonoBehaviour
{
   /// public GameObject habitEntryPrefab; // Prefab for habit entry UI element
   /// public Transform habitListContainer; // Parent object for habit entries
    //public Habit habitContainer; // Reference to the Habit script

   // private void Start()
    //{
   //     DisplayHabits();
    //}

   // public void DisplayHabits()
   // {
   //     // Clear existing habit entries
   //     foreach (Transform child in habitListContainer)
   //     {
   //         Destroy(child.gameObject);
   //     }

        // Populate the habit tracker UI with the habits list
   //     foreach (HabitData habitData in habitContainer.habits)
   //     {
   //         GameObject habitEntry = Instantiate(habitEntryPrefab, habitListContainer);
   //         habitEntry.GetComponentInChildren<Text>().text = habitData.Name;
   //         Toggle toggle = habitEntry.GetComponentInChildren<Toggle>();
   //         toggle.isOn = habitData.IsCompleted;
   //         toggle.onValueChanged.AddListener((value) => OnHabitToggleChanged(habitData, value));
    //    }
   // }

   // private void OnHabitToggleChanged(HabitData habitData, bool value)
   // {
   //     habitData.IsCompleted = value;
   //     if (value)
   //     {
            // Award XP points and coins to the player
            // Update player's XP and coins accordingly
   //     }
   // }
}