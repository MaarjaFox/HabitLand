using UnityEngine;
using UnityEngine.UI;

public class HabitInputHandler : MonoBehaviour
{
    public InputField habitInputField;

    public void OnHabitInputChanged()
    {
        string habitInput = habitInputField.text;
        HabitManager.instance.SetHabitInput(habitInput);
    }
}