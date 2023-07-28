using System;
using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour, IInputControl
{
    public event Action OnCallPauseMenu;

    [Inject] private readonly PlayerUnit _playerUnit;

    private Vector2 _moveDirection;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new(moveX, moveY);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCallPauseMenu?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        _playerUnit.Move(_moveDirection);
    }
}
