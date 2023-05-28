using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer theMixer;
    // Start is called before the first frame update
    void Start()
{
    if (PlayerPrefs.HasKey("MasterVol"))
    {
        float masterVol = PlayerPrefs.GetFloat("MasterVol");
        theMixer.SetFloat("MasterVol", masterVol);
        Debug.Log("Loaded Master volume: " + masterVol);
    }
    if (PlayerPrefs.HasKey("MusicVol"))
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVol");
        theMixer.SetFloat("MusicVol", musicVol);
        Debug.Log("Loaded Music volume: " + musicVol);
    }
    if (PlayerPrefs.HasKey("SFXVol"))
    {
        float sfxVol = PlayerPrefs.GetFloat("SFXVol");
        theMixer.SetFloat("SFXVol", sfxVol);
        Debug.Log("Loaded SFX volume: " + sfxVol);
    }
}


}
