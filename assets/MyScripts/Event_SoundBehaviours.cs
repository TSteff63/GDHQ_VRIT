using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_SoundBehaviours : MonoBehaviour
{
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.enableButtons_case4_RepairMode += PlayCase4RepairModeClip;
        GameManager.disableButtons_case4_RepairMode += StopCase4RepairModeClip;
    }

    private void PlayCase4RepairModeClip()
    {
        _audio.Play();
    }

    public void StopCase4RepairModeClip()
    {
        _audio.Stop();
    }
}
