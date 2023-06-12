using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    
    public GameObject optionsScreen;
    
    public GameObject helpScreen;

    public GameObject trackerScreen;

    public Button pauseButton; 

    public GameObject addScreen;

    public GameObject tipsScreen;

    public GameObject habitHelpScreen;

    public GameObject shopScreen;

    //text fields
    public TMP_Text levelText, hitpointText, coinsText, upgradeCostText, xpText;

    //Logic
    public Image weaponSprite;
    public RectTransform xpBar;

    //weapon upgrade
    public void OnUpgradeClick()
    {
       if(GameManager.instance.TryUpgradeWeapon())
          UpdateMenu();
    }

    //Update the character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        upgradeCostText.text = "MAX";

        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        // Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();
      

        //xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points"; //Display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int preLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXP = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXP - preLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - preLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + "/" + diff;
        }

    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                UpdateMenu();
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseButton.gameObject.SetActive(true);
        
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        pauseButton.gameObject.SetActive(false); // Deactivate the pause button
        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

     public void OpenShop()
    {
        shopScreen.SetActive(true);
    }

    public void CloseShop()
    {
        shopScreen.SetActive(false);
    }

    public void OpenHelp()
    {
        helpScreen.SetActive(true);
    }

    public void CloseHelp()
    {
        helpScreen.SetActive(false);
    }

    public void OpenTracker()
    {
        trackerScreen.SetActive(true);
    }

    public void CloseTracker()
    {
        trackerScreen.SetActive(false);
    }
    public void OpenAdd()
    {
        addScreen.SetActive(true);
    }

    public void CloseAdd()
    {
        addScreen.SetActive(false);
    }

    public void OpenTips()
    {
        tipsScreen.SetActive(true);
    }

    public void CloseTips()
    {
        tipsScreen.SetActive(false);
    }

    
}