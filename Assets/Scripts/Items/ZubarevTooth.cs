
public class ZubarevTooth : PropertyItem
{
    public override void Init()
    {
        ItemName = "��� ��������";
        Price = 350;

        AddPropertie(PlayerData.Properties.Damage, 20);
        AddPropertie(PlayerData.Properties.Health, -5);
        AddPropertie(PlayerData.Properties.Regeneration, -5);
    }
}
