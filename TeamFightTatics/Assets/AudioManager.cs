using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioSource AudioSource;
    public AudioSource CameraAudioSource;

    public AudioClip[] AudioClips;

    public Slider SFXSlider;
    public Slider BGMSlider;

    private float BGMVolume;

    #region Singleton
    public static AudioManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        AudioSource = GetComponent<AudioSource>();
        CameraAudioSource = Camera.main.GetComponent<AudioSource>();
    }
    #endregion

    private void OnLevelWasLoaded()
    {
        CameraAudioSource = Camera.main.GetComponent<AudioSource>();
        CameraAudioSource.volume = BGMVolume;
    }

    private void Update()
    {
        CameraAudioSource.volume = BGMSlider.value;
        AudioSource.volume = SFXSlider.value;
    }
}
