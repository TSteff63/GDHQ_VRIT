using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class ManagerAudioMixer : MonoBehaviour
{
    [SerializeField]
    AudioMixer masterMixer;
    AudioMixerGroup group_VO, group_Music, group_Sound;

    [SerializeField]
    Slider slider_VO, slider_Music, slider_Sound;

    public void SetVoLevel(float voLvl)
    {
        masterMixer.SetFloat("VOvol", voLvl);
    }
    public void SetMusicLevel(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }
    public void SetSFxLevel(float sfxLvl)
    {
        masterMixer.SetFloat("SFXvol", sfxLvl);
    }

}
