
public class SkillzorIsland : PropertyItem
{
    public override void Init()
    {
        Name = "Остров Скилзора";
        CurrentPrice = 25;

        AddPropertie(PlayerData.Properties.AttackSpeed, 25);
        AddPropertie(PlayerData.Properties.Dodge, -10);
    }
}
