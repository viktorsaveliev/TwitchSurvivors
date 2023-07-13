using UnityEngine;
using DG.Tweening;

public class CameraShaker
{
    private readonly PlayerUnit _player;

    private readonly Camera _camera;
    private Tween tween;

    public CameraShaker(PlayerUnit player)
    {
        _player = player;
        _camera = Camera.main;
    }

    public void Init()
    {
        _player.Health.OnTakedDamage += CameraShake;
    }

    public void DeInit()
    {
        _player.Health.OnTakedDamage -= CameraShake;
    }

    public void CameraShake()
    {
        if (tween != null) _camera.DOKill();
        tween = _camera.DOShakePosition(0.2f, 0.3f, 3, 10);
    }
}
