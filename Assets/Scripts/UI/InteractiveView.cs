using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class InteractiveView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _selectableColor;
    [SerializeField] private AudioClips _audioConfig;
    [SerializeField] private AudioSource _audio;

    private Button _button;
    private Color _originalColor;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalColor = _text.color;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _text.color = _originalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(_audioConfig.OnEnter);

        _text.DOColor(_selectableColor, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.DOColor(_originalColor, 0.5f);
    }

    private void OnClick()
    {
        PlaySound(_audioConfig.OnClick);
    }

    private void PlaySound(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }
}
