using System;
using UnityEngine;

public class Experience
{
    public event Action OnPlayerGotNewLevel;
    public event Action OnExpValueChanged;

    public int ExpForNewLevel => (int)(24 * Mathf.Pow(_expMultipleForNextLevel, Level - 1)); // Level * _expMultipleForNextLevel * 3;

    private readonly float _expMultipleForNextLevel = 3f;

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
