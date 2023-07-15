using UnityEngine;

public class Coggers : Weapon, IChargesUser
{
    [SerializeField] private Vector2[] _positionsForCoggers;
    private readonly float _rotationSpeed = 200f;

    public override void Init()
    {
        Cooldown = 10f;
        ChargesCount = 4;

        CreateCharges(true);

        for(int i = 0; i < ChargesList.Count; i++)
        {
            Cogger cogger = (Cogger)ChargesList[i];
            cogger.SetPosition(_positionsForCoggers[i]);
        }
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

        CurrentCooldown = Time.time + Cooldown;
    }
}
