using System.Collections;
using UnityEngine;

public class MikeDog : ShootableWeapon, IMoveable, IFollower
{
    private readonly float _speed = 6f;
    private readonly float _followDistance = 2f;

    private Transform _followTarget;
    private Vector3 _direction;

    private void OnEnable()
    {
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Init()
    {
        Name = "Майк";
        BasicPrice = CurrentPrice = 30;

        SetCooldown(5);
        SetDamage(5);

        CurrentChargesCount = ChargesCount = 2;
        DelayBetweenShoots = 2f;

        CreateCharges(Damage);
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                CreateCharges(Damage);
                SetDamage(10);
                break;

            case 2:
                SetCooldown(3f);
                CreateCharge(Damage);
                break;

            case 3:
                CreateCharge(Damage);
                SetDamage(15);
                break;

            case 4:
                SetDamage(20);
                break;
        }

        UpdatePrice();
        UpdateChargesDamage();
    }

    public void Move()
    {
        _direction = _followTarget.position - transform.position;
        Vector3 targetPosition = _followTarget.position - _direction.normalized * _followDistance;

        transform.position = Vector2.Lerp(transform.position, targetPosition, _speed * Time.fixedDeltaTime);
    }

    public void SetFollowTarget(Transform target)
    {
        _followTarget = target;
    }

    protected override void SetNextTarget(IEnemyCounter enemyCounter)
    {
        
    }

    protected override IEnumerator ShootBehaviour(IEnemyCounter enemyCounter)
    {
        WaitForSeconds delayBetweenShoots = new(DelayBetweenShoots);

        for (int i = 0; i < ChargesCount; i++)
        {
            if (ChargesList[i].gameObject.activeSelf) continue;

            ChargesList[i].Shoot(transform.position, null, BulletSpeed);
            yield return delayBetweenShoots;
        }

        OnChargesEnded();
    }
}
