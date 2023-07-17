using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField] private Unit _currentUnit;

    private void Start()
    {
        _currentUnit.Health.OnTakedDamage += EnableDamageCounter;
    }

    private void EnableDamageCounter(int damage)
    {
        TMP_Text text = FindFreeTextObject();
        text.rectTransform.anchoredPosition = Vector2.zero;
        text.text = $"{damage}";
        text.gameObject.SetActive(true);
        text.rectTransform.DOAnchorPos(new Vector2(-0.718f, -0.378f), 0.3f).OnComplete(() 
            => text.gameObject.SetActive(false));
    }

    private TMP_Text FindFreeTextObject()
    {
        TMP_Text freeText = null;

        foreach (TMP_Text text in _texts)
        {
            if (text.gameObject.activeSelf) continue;
            freeText = text;
            break;
        }

        if (freeText == null)
        {
            DisableAll();
            freeText = _texts[0];
        }

        return freeText;
    }

    private void DisableAll()
    {
        foreach (TMP_Text text in _texts)
        {
            text.gameObject.SetActive(false);
        }
    }
}
