
public class LoafDog : PropertyItem
{
    public override void Init()
    {
        Name = "� ��";
        Price = 150;

        AddPropertie(PlayerData.Properties.Fortune, 5);
        AddPropertie(PlayerData.Properties.Health, 5);
        AddPropertie(PlayerData.Properties.Regeneration, 5);
    }
}
