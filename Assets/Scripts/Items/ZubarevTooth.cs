
public class ZubarevTooth : PropertyItem
{
    public override void Init()
    {
        Name = "��� ��������";
        CurrentPrice = 15;

        AddPropertie(PlayerData.Properties.Damage, 20);
        AddPropertie(PlayerData.Properties.Health, -5);
        AddPropertie(PlayerData.Properties.Regeneration, -5);
    }
}
