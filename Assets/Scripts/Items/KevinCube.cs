
public class KevinCube : PropertyItem
{
    public override void Init()
    {
        Name = "��� �����";
        CurrentPrice = 45;

        AddPropertie(PlayerData.Properties.Health, 15);
        AddPropertie(PlayerData.Properties.Damage, 5);
    }
}
