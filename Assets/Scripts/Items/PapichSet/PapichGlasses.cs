
public class PapichGlasses : PropertyItem
{
    public override void Init()
    {
        Name = "���� ������";
        CurrentPrice = 100;

        AddPropertie(PlayerData.Properties.Distance, 5);
    }
}
