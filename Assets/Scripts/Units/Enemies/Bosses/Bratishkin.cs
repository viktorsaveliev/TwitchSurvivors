using DG.Tweening;
using UnityEngine;

public class Bratishkin : Enemy, IBoss
{
    [SerializeField] private EnemyBlaster _weaponPrefab;

    private EnemyBlaster _weapon;
    private Collider2D _fightArea;

    private int _step = 0;

    public void SetFightArea(Collider2D area)
    {
        _fightArea = area;
    }

    public override void Init()
    {
        base.Init();
        Nickname = "Братишкин";
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

        _weapon = Instantiate(_weaponPrefab, transform);
        _weapon.Init();
    }

    private void Attack()
    {
        if (!gameObject.activeSelf) return;

        _weapon.Shoot();
    }

    private void Action()
    {
        if (!gameObject.activeSelf) return;

        switch (_step)
        {
            case 0:
            case 1:
                _step++;

                Vector3 posFromTarget = GetRandomPositionFromTarget(Target.transform.position);
                JumpTo(posFromTarget);
                Invoke(nameof(Action), 2);
                break;

            case 2:
                _step = 0;

                Attack();
                Invoke(nameof(Action), 2);
                break;
        }
    }

    private void JumpTo(Vector3 position)
    {
        IsCanMove = false;

        Animator.SetTrigger("Jump");

        transform.DOMove(position, 1.5f).SetEase(Ease.InOutBack)
            .OnComplete(() => 
            {
                IsCanMove = true;
            });
    }

    /*private void MoveToRandomPoint()
    {
        if (_fightArea == null || !gameObject.activeSelf) return;

        IsCanMove = false;

        Bounds bounds = _fightArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 enemyPosition = new(randomX, randomY);

        JumpTo(enemyPosition);

        Invoke(nameof(MoveToRandomPoint), 7);
    }*/

    private Vector3 GetRandomPositionFromTarget(Vector3 targetPosition)
    {
        float minDistance = 2f;
        float maxDistance = 5f;

        float randomAngleX = Random.Range(0f, 360f);
        float randomAngleY = Random.Range(0f, 360f);

        Vector3 randomDirection = new(  Mathf.Sin(randomAngleX) * Mathf.Cos(randomAngleY),
                                        Mathf.Sin(randomAngleY),
                                        0);

        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 randomPosition = targetPosition + randomDirection * randomDistance;
        bool isSpawnPointInArea = _fightArea.OverlapPoint(randomPosition);

        return isSpawnPointInArea ? randomPosition : Vector2.zero;
    }
}
