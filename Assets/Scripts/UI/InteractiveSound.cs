using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InteractiveSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClips _audioConfig;
    [SerializeField] private AudioSource _audio;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(_audioConfig.OnEnter);
    }

    private void OnClick()
    {
        PlaySound(_audioConfig.OnClick);
    }

    private void PlaySound(AudioClip clip)
    {
        if (!_audio.gameObject.activeSelf) return;
        _audio.clip = clip;
        _audio.Play();
    }
}
