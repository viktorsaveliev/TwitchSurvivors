using UnityEngine;

public class Coggers : Weapon, IChargesUser
{
    [SerializeField] private Vector2[] _positionsForCoggers;
    private readonly float _rotationSpeed = 200f;

    public override void Init()
    {
        SetDamage(5);
        SetCooldown(10f);
        ChargesCount = 2;

        CreateCharges(Damage, true);
        UpdatePositionForCoggers();
    }

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    public void Shoot(IEnemyCounter enemyDetection)
    {
        if (Time.time < CurrentCooldown) return;

        foreach (Bullet charge in ChargesList)
        {
            charge.Shoot(Vector2.zero, Vector2.zero);
        }

        ActivateCooldown();
    }

    protected override void Improve()
    {
        if (ImprovementLevel > 4) return;
        ImprovementLevel++;

        switch (ImprovementLevel)
        {
            case 1:
                SetCooldown(8f);
                SetDamage(10);
                break;

            case 2:
                SetCooldown(7f);
                CreateCharge(Damage, true);
                UpdatePositionForCoggers();
                break;

            case 3:
                SetCooldown(6f);
                SetDamage(15);
                break;

            case 4:
                CreateCharge(Damage, true);
                UpdatePositionForCoggers();
                SetDamage(20);
                break;
        }

        UpdateChargesDamage();
    }

    private void UpdatePositionForCoggers()
    {
        for (int i = 0; i < ChargesList.Count; i++)
        {
            Cogger cogger = (Cogger)ChargesList[i];
            cogger.SetPosition(_positionsForCoggers[i]);
        }
    }
}
