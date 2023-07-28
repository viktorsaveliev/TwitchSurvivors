using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;

    protected string Name;
    protected int CurrentPrice;
    protected int BasicPrice;

    public Sprite GetIcon() => _icon;
    public string GetName() => Name;
    public int GetPrice() => CurrentPrice;

    public abstract void Init();
    public abstract void Use();
    public abstract void UnUse();
    public void DestroyItem() => Destroy(gameObject);
}
