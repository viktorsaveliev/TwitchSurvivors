
public class LoafDog : Item
{
    public override void Init()
    {
        ItemName = "������ �������";
        Price = 150;

        AddPropertie(PlayerData.Properties.Fortune, 5);
        AddPropertie(PlayerData.Properties.Health, 5);
        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
