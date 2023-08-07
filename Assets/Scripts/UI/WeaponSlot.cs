using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private bool _isUsed;

    public bool IsUsed => _isUsed;

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.transform.localScale = new Vector2(0.6f, 0.6f);
        _icon.SetNativeSize();
        _icon.gameObject.SetActive(true);

        _isUsed = true;
    }
}
