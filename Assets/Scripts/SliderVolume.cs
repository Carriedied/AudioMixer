using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderVolume : MonoBehaviour
{
    [SerializeField] private string _mixerGroupName;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioPlayback _toggleSound;

    private Slider _changingVolumeSlider;

    private void Awake()
    {
        _changingVolumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _changingVolumeSlider.onValueChanged.AddListener(SetVolume);

        float savedVolume = PlayerPrefs.GetFloat(_mixerGroupName, 0f);

        _changingVolumeSlider.value = savedVolume;
    }

    private void SetVolume(float value)
    {
        if (!_toggleSound.SoundsEnabled)
        {
            _audioMixer.SetFloat(_mixerGroupName, Mathf.Log10(value) * 20);
        }

        PlayerPrefs.SetFloat(_mixerGroupName, value);
    }
}
