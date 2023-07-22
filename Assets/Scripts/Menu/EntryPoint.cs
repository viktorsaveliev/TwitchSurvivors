using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [Inject] private readonly PlayerData _playerData;

    private void Awake()
    {
        _playerData.Init();
    }
}
