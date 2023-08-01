using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Projectile89 : Projectile, IMoveable
{
    public override void Init(int damage)
    {
        base.Init(damage);
        OnHitPlayer += Thief;
    }

    private void Thief(PlayerUnit unit)
    {
        unit.Weapons.HideWeapons(5f);
    }
}
