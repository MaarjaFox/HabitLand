using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    //Resources
    public List<Sprite> playerSprites;
    public List <int> xpTable;

    //References

    public Player player;

    //Logic
    public int coins;
    public int experience;

    public void SaveState()
    {
       // string s = "";

       // s += "0" + "|";
       // s += coins.ToString() + "|";
       // s += experience.ToString() + "|";

        //PlayerPrefs.SetString("SaveState, s");
    }

    public void LoadState()
    {
       // string[] data = PlayerPrefs.GetString("SaveState").Split('|');
       // Debug.Log("LoadState");
    }
}
