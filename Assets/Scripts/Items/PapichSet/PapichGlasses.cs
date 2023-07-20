
public class PapichGlasses : PropertyItem
{
    public override void Init()
    {
        ItemName = "Очки Папича";
        Price = 100;

        AddPropertie(PlayerData.Properties.Distance, 5);
    }
}
