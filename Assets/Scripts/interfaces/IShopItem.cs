using UnityEngine;

public interface IShopItem
{
    public abstract void Use();
    public abstract void UnUse();
    public abstract void DestroyObject();

    public Sprite GetIcon();
    public string GetName();
    public int GetPrice();
}
