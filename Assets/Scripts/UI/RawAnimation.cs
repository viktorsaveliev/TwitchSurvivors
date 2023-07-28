using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawAnimation : MonoBehaviour
{
    [SerializeField] private float[] speed;
    private RawImage _background;
    
    private void Start()
    {
        _background = GetComponent<RawImage>();
    }

    private void FixedUpdate()
    {
        _background.uvRect = new Rect(_background.uvRect.x + speed[0], _background.uvRect.y + speed[1], 1, 1);
    }
}