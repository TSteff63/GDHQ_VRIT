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
    public AudioSource _SFXSource;
    [SerializeField]
    public AudioClip[] _VO;
    [SerializeField]
    public AudioClip[] _music;
    [SerializeField]
    public AudioClip[] _SFX;

    ///
    /// Using singleton to trigger when audio will begin, detect when clips end.
    ///

    private void Awake()
    {
        _instance = this;
    }

    public void Start()
    {
        if(_playAudioAtStart)
        PlayVOClip(0);
    }

    public void PlayVOClip(int clip)
    {
        _SFXSource.Stop();
        _VOSource.clip = _VO[clip];
        _VOSource.Play();
    }
    public void PlayMusicClip(int clip)
    {
        _SFXSource.Stop();
        _MusicSource.clip = _music[clip];
        _MusicSource.Play();
    }
    public void PlaySFXClip(int clip)
    {
        _SFXSource.Stop();
        _SFXSource.clip = _SFX[clip];
        _SFXSource.Play();
    }
}