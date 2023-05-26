using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject habitTrackerPanel; // Reference to the habit tracker panel

    private bool isHabitTrackerOpen;

    // Called when the pause menu is opened
    private void OnEnable()
    {
        isHabitTrackerOpen = false;
        habitTrackerPanel.SetActive(false);
    }

    // Toggle the habit tracker panel
    public void ToggleHabitTracker()
    {
        isHabitTrackerOpen = !isHabitTrackerOpen;
        habitTrackerPanel.SetActive(isHabitTrackerOpen);
    }
}