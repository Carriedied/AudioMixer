using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    [SerializeField] private AudioClip _melody;
    [SerializeField] private AudioSource _audioSource;

    private Button _musicButton;

    private void Awake()
    {
        _musicButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _musicButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _musicButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (_melody == null)
            return;

        _audioSource.PlayOneShot(_melody);
    }
}
