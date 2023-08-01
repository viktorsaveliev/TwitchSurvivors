using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private bool _isUsed;

    public bool IsUsed => _isUsed;

    public void SetIcon(Sprite icon)
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = icon;
        _isUsed = true;
    }
}
