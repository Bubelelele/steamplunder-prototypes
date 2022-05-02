using UnityEngine;

public class HeavyEnemy : EnemyBase
{
    public override void EnemyInSight()
    {
        Debug.Log("Heavy");
    }
    public override void EnemyOutOfSight()
    {
        Debug.Log("NotHeavy");
    }
}
