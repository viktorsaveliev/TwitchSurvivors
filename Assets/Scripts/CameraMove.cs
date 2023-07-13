using UnityEngine;
using Zenject;

public class CameraMove : MonoBehaviour
{
    [Inject] private readonly PlayerUnit _playerUnit;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Transform _target;
    private Vector3 _desiredPosition;

    private void Awake()
    {
        _target = _playerUnit.transform;
    }

    private void FixedUpdate()
    {
        _desiredPosition = _target.position + _offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
