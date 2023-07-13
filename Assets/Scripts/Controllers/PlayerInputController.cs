using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;

    private Vector2 _moveDirection;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = new(moveX, moveY);
    }

    private void FixedUpdate()
    {
        _playerUnit.Move(_moveDirection);
    }
}
