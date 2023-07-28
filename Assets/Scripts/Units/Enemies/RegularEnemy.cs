using UnityEngine;

public class RegularEnemy : Enemy
{
    private void FixedUpdate()
    {
        if(IsCanMove && Target != null)
        {
            Vector2 direction = Target.position - transform.position;
            Move(direction);
        }
    }
}
