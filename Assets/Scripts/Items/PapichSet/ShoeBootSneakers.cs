
public class ShoeBootSneakers : PropertyItem
{
    public override void Init()
    {
        Name = "Туфло-Ботинки-Кроссовки";
        CurrentPrice = 20;

        AddPropertie(PlayerData.Properties.MoveSpeed, 5);
    }
}
