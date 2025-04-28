using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderVolume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioPlayback _toggleSound;

    private Slider _changingVolumeSlider;

    private void Awake()
    {
        _changingVolumeSlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _changingVolumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDisable()
    {
        _changingVolumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(_audioMixerGroup.name, 0f);

        _changingVolumeSlider.value = savedVolume;
    }

    private void SetVolume(float value)
    {
        if (!_toggleSound.SoundsEnabled)
        {
            _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, Mathf.Log10(value) * 20);
        }

        PlayerPrefs.SetFloat(_audioMixerGroup.name, value);
    }
}
