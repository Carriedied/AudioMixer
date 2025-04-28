using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioPlayback : MonoBehaviour
{
    private const string MasterAudioMixerName = "Master";

    [SerializeField] private AudioMixer _audioMixer;

    public bool SoundsEnabled { get; private set; }

    private Button _toggleButton;

    private float _minVolume = -80f;
    private float _maxVolume = 0f;

    private void Awake()
    {
        _toggleButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _toggleButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _toggleButton.onClick.RemoveListener(OnButtonClick);
    }

    private void Start()
    {
        SoundsEnabled = false; 
    }

    private void OnButtonClick()
    {
        SoundsEnabled = !SoundsEnabled;

        if (SoundsEnabled)
        {
            _audioMixer.SetFloat(MasterAudioMixerName, _minVolume);
        }
        else
        {
            _audioMixer.SetFloat(MasterAudioMixerName, _maxVolume);
        }
    }
}
