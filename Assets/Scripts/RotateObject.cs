using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 70f;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
