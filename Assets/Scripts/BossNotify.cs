
public class BossNotify
{
    private readonly Notify _notify;
    private readonly EnemySpawner _spawner;

    public BossNotify(Notify notify, EnemySpawner spawner)
    {
        _notify = notify;
        _spawner = spawner;
    }

    public void Init()
    {
        _spawner.OnTick += Notify;
    }

    private void Notify(float time)
    {
        if (time == 296)
        {
            _notify.Show("¬Õ»Ã¿Õ»≈! ¡Œ—— ¡À»« Œ!!!");
        }
    }
}
