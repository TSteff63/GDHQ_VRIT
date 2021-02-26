using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    //bool for dev
    [SerializeField]
    private bool _playAudioAtStart;

    private static MyAudioManager _instance;
    public static MyAudioManager Instance
    {
        get 
        {
            if (_instance == null)
                Debug.LogError("The audio manager is NULL");

            return _instance;
        }
    }

    [SerializeField]
    public AudioSource _VOSource;
    [SerializeField]
    public AudioSource _MusicSource;
    [SerializeField]
    public AudioClip[] _VO;
    [SerializeField]
    public AudioClip[] _music;

    ///
    /// Using singleton to trigger when audio will begin, detect when clips end.
    ///

    public void Start()
    {
        if(_playAudioAtStart)
        PlayVOClip(_VO[0]);
    }

    public void PlayVOClip(AudioClip clip)
    {
        _VOSource.clip = clip;
        _VOSource.Play();
    }
    public void PlayMusicClip(AudioClip clip)
    {
        _MusicSource.clip = clip;
        _MusicSource.Play();
    }

}
