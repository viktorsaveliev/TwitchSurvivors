using System;

public class Experience
{
    public event Action OnPlayerGotNewLevel;
    public event Action OnExpValueChanged;

    public int ExpForNewLevel => Level * _expMultipleForNextLevel * 3; 

    private readonly int _expMultipleForNextLevel = 8;

    public int Level { get; private set; }
    public int Exp { get; private set; }

    public void Init()
    {
        Level = 1;
        Exp = 0;
    }

    public void GiveExp(int value)
    {
        if (value < 1 || value > 50) return;

        Exp += value;

        if (Exp >= ExpForNewLevel)
        {
            Level++;
            Exp = 0;

            OnPlayerGotNewLevel?.Invoke();
        }

        OnExpValueChanged?.Invoke();
    }
}
