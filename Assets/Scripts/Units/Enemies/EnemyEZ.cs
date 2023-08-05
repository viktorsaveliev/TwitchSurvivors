using UnityEngine;

public class EnemyEZ : Enemy
{
    public override void Init()
    {
        base.Init();
        Health.OnImmunityDamage += DropGlassess;
    }

    private void DropGlassess()
    {
        Animator.SetTrigger("DropGlassess");
    }
}
