using DG.Tweening;
using UnityEngine;

public class WhiteRa : Enemy, IBoss
{
    [SerializeField] private EnemyBlaster _weaponPrefab;
    [SerializeField] private ExplodeZone _explodeZone;

    private const int _explodeZonesCount = 10;

    private readonly ExplodeZone[] _explodeZones = new ExplodeZone[_explodeZonesCount];

    private EnemyBlaster _blaster;
    private Collider2D _fightArea;

    private int _step = 0;

    public void SetFightArea(Collider2D area)
    {
        _fightArea = area;
    }

    public override void Init()
    {
        base.Init();
        Nickname = "White Ra";
        CreateWeapon();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        Invoke(nameof(Action), 3);
    }

    private void CreateWeapon()
    {
        if (_weaponPrefab == null) return;

        _blaster = Instantiate(_weaponPrefab, transform);
        _blaster.Init();

        for (int i = 0; i < _explodeZonesCount; i++)
        {
            _explodeZones[i] = Instantiate(_explodeZone);
            _explodeZones[i].Init(2f, 25);
        }
    }

    private void Attack()
    {
        if (!gameObject.activeSelf) return;

        _blaster.Shoot();
    }

    private void Action()
    {
        if (!gameObject.activeSelf) return;

        switch (_step)
        {
            case 0:
                _step++;
                Invoke(nameof(Action), 4);
                break;
            case 1:
                _step++;

                Attack();
                Invoke(nameof(Action), 1);
                break;

            case 2:
                _step++;

                Attack();
                Invoke(nameof(Action), 4);
                break;

            case 3:
                _step = 0;

                for (int i = 0; i < _explodeZonesCount; i++)
                {
                    _explodeZones[i].Active(GetRandomPoint());
                }

                Animator.SetTrigger("UseBanHammer");
                Invoke(nameof(Action), 4);
                break;
        }
    }

    private void JumpTo(Vector3 position)
    {
        IsCanMove = false;

        transform.DOMove(position, 3f).SetEase(Ease.InOutBack)
            .OnComplete(() => 
            {
                IsCanMove = true;
            });
    }

    private Vector2 GetRandomPoint()
    {
        if (_fightArea == null || !gameObject.activeSelf) return Vector2.zero;

        Bounds bounds = _fightArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 position = new(randomX, randomY);

        return position;
    }
}
