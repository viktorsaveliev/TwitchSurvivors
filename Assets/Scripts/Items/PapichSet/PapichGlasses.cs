
public class PapichGlasses : Item
{
    public override void Init()
    {
        ItemName = "���� ������";
        Price = 100;

        AddPropertie(PlayerData.Properties.AttackDistance, 5);
    }
}
