
public class Stray : PropertyItem
{
    public override void Init()
    {
        ItemName = "Рыжий камень";
        Price = 400;

        AddPropertie(PlayerData.Properties.Greed, 10);
        AddPropertie(PlayerData.Properties.Dodge, 10);

        AddPropertie(PlayerData.Properties.Distance, -10);
    }
}
