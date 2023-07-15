using UnityEngine;
using Zenject;

[RequireComponent(typeof(CircleCollider2D))]
public class BitsDetection : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    private CircleCollider2D _circleCollider;

    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetDetectionRadius(float value)
    {
        if (value < 1 || value > 9) return;
        _circleCollider.radius = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bits bits))
        {
            bits.Pickup(_player);
        }
    }
}
