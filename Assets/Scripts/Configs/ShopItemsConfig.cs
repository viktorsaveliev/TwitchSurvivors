using UnityEngine;

[CreateAssetMenu(fileName = "ShopItems", menuName = "Shop Items Config")]
public class ShopItemsConfig : ScriptableObject
{
    [SerializeField] private GameObject[] _shopItems;
    public GameObject[] ShopItems => _shopItems;
}
