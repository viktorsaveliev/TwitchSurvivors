using UnityEngine;
using Zenject;

[RequireComponent(typeof(CircleCollider2D))]
public class BitsDetection : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _player;

    public readonly float OriginalRadius = 1.7f;
    private CircleCollider2D _circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bits bits))
        {
            bits.Pickup(_player);
        }
    }

    public void Init()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetDetectionRadius(float value)
    {
        if (value < 1 || value > 25) return;
        _circleCollider.radius = value;
    }
}
