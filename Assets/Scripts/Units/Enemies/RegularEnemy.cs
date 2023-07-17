using UnityEngine;

public class RegularEnemy : Enemy
{
    public override void Init(Transform target)
    {
        base.Init(target);

        RegularSpeed = CurrentSpeed = 3;
        Damage = 10;

        Health.SetMaxHealth(10, true);
    }

    private void FixedUpdate()
    {
        if(IsCanMove)
        {
            Vector2 direction = Target.position - transform.position;
            Move(direction);
        }
    }
}
