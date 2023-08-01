using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class Notify : MonoBehaviour
{
    [SerializeField] private TMP_Text _notify;

    private readonly float _notifyDuration = 3f;
    private readonly float _openCloseDuration = 1f;

    public void Show(string text)
    {
        _notify.text = text;
        _notify.gameObject.SetActive(true);

        Transform textTransform = _notify.transform;
        textTransform.localScale = Vector2.zero;
        textTransform.DOScale(1, _openCloseDuration);

        _notify.rectTransform.DOAnchorPosY(16.61f, _openCloseDuration).SetEase(Ease.OutBounce);

        StartCoroutine(DelayForHide());
    }

    public void Hide()
    {
        _notify.transform.DOScale(0, _openCloseDuration);
        _notify.rectTransform.DOAnchorPosY(-159f, _openCloseDuration).SetEase(Ease.InBack)
            .OnComplete(() => _notify.gameObject.SetActive(false));
    }

    private IEnumerator DelayForHide()
    {
        yield return new WaitForSeconds(_notifyDuration);
        Hide();
    }
}
