using UnityEngine;

public class CombineEnemyAndTwitch
{
    private readonly TwitchIntegration _twitch;
    private readonly EnemySpawner _enemySpawner;

    public CombineEnemyAndTwitch(TwitchIntegration twitch, EnemySpawner enemySpawner)
    {
        _twitch = twitch;
        _enemySpawner = enemySpawner;
    }

    public void Init()
    {
        _enemySpawner.OnEnemySpawned += ChangeEnemyNickname;
    }

    public void DeInit()
    {
        _enemySpawner.OnEnemySpawned -= ChangeEnemyNickname;
    }

    private void ChangeEnemyNickname(Enemy enemy)
    {
        string nickname = "anon2004";
        if (_twitch.ChatNicknames.Count > 0)
        {
            nickname = _twitch.ChatNicknames[Random.Range(0, _twitch.ChatNicknames.Count)];
        }

        enemy.Nickname = nickname;
    }
}
