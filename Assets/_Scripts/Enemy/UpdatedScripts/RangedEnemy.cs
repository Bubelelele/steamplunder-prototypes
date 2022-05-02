using UnityEngine;

public class RangedEnemy : EnemyBase
{
    public override void EnemyInSight()
    {
        Debug.Log("Ranged");
    }
    public override void EnemyOutOfSight()
    {
        Debug.Log("NotRanged");
    }
}
