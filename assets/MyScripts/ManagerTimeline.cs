using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ManagerTimeline : MonoBehaviour
{
    private static ManagerTimeline _instance;
    public static ManagerTimeline Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The timeline manager is NULL");

            return _instance;
        }
    }

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;


    private void Awake()
    {
        _instance = this;
    }

    //public static Action OnPlayTimeline;

    /*
    public void Play()
    {
        foreach (PlayableDirector playableDirector in playableDirectors)
        {
            playableDirector.Play();
        }
    }
    */


    public void PlayFromTimelines(int index)
    {
        Debug.Log("Play Timeline Sequence");

        TimelineAsset selectedAsset;

        if (timelines.Count <= index)
        {
            selectedAsset = timelines[timelines.Count - 1];
        }
        else
        {
            selectedAsset = timelines[index];
        }

        playableDirectors[0].Play(selectedAsset);
    }
}
