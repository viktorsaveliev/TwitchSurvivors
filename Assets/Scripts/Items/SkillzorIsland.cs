
public class SkillzorIsland : Item
{
    public override void Init()
    {
        ItemName = "Остров Скилзора";
        Price = 150;

        AddPropertie(PlayerData.Properties.AttackSpeed, 25);
        AddPropertie(PlayerData.Properties.Dodge, -10);
    }
}
