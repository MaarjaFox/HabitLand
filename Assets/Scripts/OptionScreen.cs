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

    float vol = 0f;
    theMixer.GetFloat("MasterVol", out vol);
    mastSlider.value = vol;
    theMixer.GetFloat("MusicVol", out vol);
    musicSlider.value = vol;
    theMixer.GetFloat("SFXVol", out vol);
    sfxSlider.value = vol;
    
    mastLabel.text = (mastSlider.value + 80).ToString();
    musicLabel.text = (musicSlider.value + 80).ToString();
    sfxLabel.text = (sfxSlider.value + 80).ToString();
    
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
   }
    public void SetMusicVol()
   {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
   }
    public void SetSFXVol()
   {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
   }
}
