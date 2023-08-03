using System;
using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour, IInputControl
{
    public event Action OnCallPauseMenu;

    [Inject] private readonly PlayerUnit _playerUnit;
    [SerializeField] private Transform[] _zoneEdges = new Transform[4];

    private Vector2 _moveDirection;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = _playerUnit.transform;
    }

    private enum Edge
    {
        Left,
        Right,
        Down,
        Up
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX < 0 && _playerTransform.position.x <= _zoneEdges[(int)Edge.Left].position.x ||
           moveX > 0 && _playerTransform.position.x >= _zoneEdges[(int)Edge.Right].position.x)
        {
            moveX = 0;
        }

        if (moveY > 0 && _playerTransform.position.y >= _zoneEdges[(int)Edge.Up].position.y ||
           moveY < 0 && _playerTransform.position.y <= _zoneEdges[(int)Edge.Down].position.y)
        {
            moveY = 0;
        }

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
