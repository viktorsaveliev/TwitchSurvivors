using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject] private readonly PlayerData _playerData;

    private void Awake()
    {
        _playerData.Init();
    }

    private void Start()
    {
        SceneLoader loader = new();
        loader.LoadMenu();
    }
}
