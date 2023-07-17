
public class ShoeBootSneakers : Item
{
    public override void Init()
    {
        ItemName = "Туфло-Ботинки-Кроссовки";
        Price = 100;

        AddPropertie(PlayerData.Properties.MoveSpeed, 5);
    }
}
