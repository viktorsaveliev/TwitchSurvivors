
public class ShoeBootSneakers : PropertyItem
{
    public override void Init()
    {
        Name = "Туфло-Ботинки-Кроссовки";
        Price = 100;

        AddPropertie(PlayerData.Properties.MoveSpeed, 5);
    }
}
