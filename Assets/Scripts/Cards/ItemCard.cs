using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemCard : ShopCard
{
    private PropertyItem _item;

    public override void Init(Item item)
    {
        base.Init(item);

        _item = (PropertyItem) item;
        UpdateStats();
    }

    public override void UpdateStats()
    {
        base.UpdateStats();

        int index = 0;
        foreach (var pair in _item.Properties)
        {
            string percent = pair.Value > 0 ? $"<color=green>+{pair.Value}</color>" : $"<color=red>{pair.Value}</color>";
            PropertiesText[index].text = $"{PlayerData.PropertiesName[(int)pair.Key]}: {percent}%";
            index++;
        }

        for (int i = index; i < PropertiesText.Length; i++)
        {
            PropertiesText[i].text = string.Empty;
        }
    }
}
