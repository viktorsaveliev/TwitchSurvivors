
public class PapichGlasses : PropertyItem
{
    public override void Init()
    {
        ItemName = "���� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.Distance, 5);
    }
}
