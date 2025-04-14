using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioClip _soundGameEnvironment;
    [SerializeField] private AudioClip _soundFunMusic;
    [SerializeField] private AudioClip _soundOfVictory;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderButtons;
    [SerializeField] private Slider _sliderMusic;

    private bool _soundsEnabled = true;
    private float _minVolume = -80f;

    private void Start()
    {
        _sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        _sliderButtons.onValueChanged.AddListener(SetButtonVolume);
        _sliderMusic.onValueChanged.AddListener(SetMusicVolume);

        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float savedButtonsVolume = PlayerPrefs.GetFloat("ButtonsVolume", 1f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        _sliderMaster.value = savedMasterVolume;
        _sliderButtons.value = savedButtonsVolume;
        _sliderMusic.value = savedMusicVolume;

        SetMasterVolume(savedMasterVolume);
        SetButtonVolume(savedButtonsVolume);
        SetMusicVolume(savedMusicVolume);
    }

    public void PlaySound(int soundIndex)
    {
        if (!_soundsEnabled) return;

        switch (soundIndex)
        {
            case 1:
                _audioSource.PlayOneShot(_soundGameEnvironment);
                break;
            case 2:
                _audioSource.PlayOneShot(_soundFunMusic);
                break;
            case 3:
                _audioSource.PlayOneShot(_soundOfVictory);
                break;
        }
    }

    public void ToggleSounds()
    {
        _soundsEnabled = !_soundsEnabled;

        if (!_soundsEnabled)
        {
            _audioMixer.SetFloat("MasterVolume", _minVolume);
        }
        else
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        }
    }

    private void SetMasterVolume(float value)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);

        if (!_soundsEnabled)
            _soundsEnabled = !_soundsEnabled;

        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void SetButtonVolume(float value)
    {
        _audioMixer.SetFloat("ButtonsVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("ButtonsVolume", value);
    }

    private void SetMusicVolume(float value)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}
