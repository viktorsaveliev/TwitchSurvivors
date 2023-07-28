
public class ShoeBootSneakers : PropertyItem
{
    public override void Init()
    {
        Name = "Туфло-Ботинки-Кроссовки";
        CurrentPrice = 100;

        AddPropertie(PlayerData.Properties.MoveSpeed, 5);
    }
}
