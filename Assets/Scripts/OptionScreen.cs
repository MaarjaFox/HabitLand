using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionScreen : MonoBehaviour
{
   public Toggle fullsceenTog;

   public AudioMixer theMixer;

   public TMP_Text mastLabel, musicLabel, sfxLabel;
   public Slider mastSlider, musicSlider, sfxSlider;

   void Start()
   {
    fullsceenTog.isOn = Screen.fullScreen;

    // Retrieve the saved volume values from PlayerPrefs
    float masterVol = PlayerPrefs.GetFloat("MasterVol", 0f);
    float musicVol = PlayerPrefs.GetFloat("MusicVol", 0f);
    float sfxVol = PlayerPrefs.GetFloat("SFXVol", 0f);

    // Update the slider positions
    mastSlider.value = masterVol;
    musicSlider.value = musicVol;
    sfxSlider.value = sfxVol;

    // Apply the updated volume settings to the audio mixer
    theMixer.SetFloat("MasterVol", masterVol);
    theMixer.SetFloat("MusicVol", musicVol);
    theMixer.SetFloat("SFXVol", sfxVol);

    // Update the label texts
    mastLabel.text = (masterVol + 80).ToString();
    musicLabel.text = (musicVol + 80).ToString();
    sfxLabel.text = (sfxVol + 80).ToString();
   }

   public void ApplyGraphics()
   {
    Screen.fullScreen = fullsceenTog.isOn;
    Debug.Log("Fullscreen");
   }

   public void SetMasterVol()
   {
        mastLabel.text = (mastSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", mastSlider.value);

        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);

        PlayerPrefs.Save();
   }
    public void SetMusicVol()
   {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.Save();
   }
    public void SetSFXVol()
   {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.Save();
   }
}
