using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Sword : Weapon, IChargesUser
{
    private readonly float _rotationSpeed = 500f;

    private readonly Vector2[] _improvementLevelScale =
    {
        new Vector2(1.5f, 1.5f),
        new Vector2(2f, 2f),
        new Vector2(2.5f, 2.5f),
        new Vector2(3f, 3f),
        new Vector2(3.5f, 3.5f)
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(GetDamageValue());
        }
    }

    public override void Init()
    {
        Name = "Меч модератора";
        BasicPrice = CurrentPrice = 50;

        SetDamage(10);
        SetCooldown(3f);

        CreateCharge(Damage, true);
        ChargesCount = CurrentChargesCount = 1;
    }

    public void Shoot(IEnemyCounter enemyCounter)
    {
        if (Time.time < CurrentCooldown) return;

        StartRotation();
    }

    public override void Improve()
    {
        if (ImprovementLevel >= MAX_IMPROVE_LEVEL) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                ChargesCount = 2;
                SetDamage(15);
                break;

            case 2:
                ChargesCount = 3;
                SetDamage(30);
                break;

            case 3:
                ChargesCount = 4;
                SetCooldown(2f);
                break;

            case 4:
                SetDamage(45);
                SetCooldown(1f);
                break;
        }

        UpdatePrice();
    }

    private void StartRotation()
    {
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        IsActive = true;
        ChargesList[0].gameObject.SetActive(true);
        ChargesList[0].transform.DOScale(_improvementLevelScale[ImprovementLevel], 0.3f);

        for (int i = 0; i < ChargesCount; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            float totalRotation = 0;

            while (totalRotation < 360f)
            {
                float rotationStep = _rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, rotationStep);

                totalRotation += rotationStep;

                yield return null;
            }

            CurrentChargesCount--;
            yield return null;
        }

        ChargesList[0].transform.DOScale(0, 0.3f)
            .OnComplete(() => ChargesList[0].gameObject.SetActive(false));

        IsActive = false;
        CurrentChargesCount = ChargesCount;

        ActivateCooldown();
    }
}
