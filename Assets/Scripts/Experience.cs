using System;
using UnityEngine;

public class Experience
{
    public event Action OnPlayerGotNewLevel;
    public event Action OnExpValueChanged;

    public int ExpForNewLevel => (int)(24 * Mathf.Pow(_expMultipleForNextLevel, Level - 1)); // Level * _expMultipleForNextLevel * 3;

    private readonly float _expMultipleForNextLevel = 1.3f;

    public int Level { get; private set; }
    public int Value { get; private set; }

    public void Init()
    {
        Level = 1;
        Value = 0;
    }

    public void GiveExp(int value)
    {
        if (value < 1 || value > 50) return;

        Value += value;

        if (Value >= ExpForNewLevel)
        {
            Level++;
            Value = 0;

            OnPlayerGotNewLevel?.Invoke();
        }

        OnExpValueChanged?.Invoke();
    }
}
