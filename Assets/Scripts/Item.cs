using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;

    protected string ItemName;
    protected int Price;

    public Sprite GetIcon() => _icon;
    public string GetName() => ItemName;
    public int GetPrice() => Price;

    public abstract void Init();
    public abstract void Use();
    public abstract void UnUse();
    public void DestroyItem() => Destroy(gameObject);
}
