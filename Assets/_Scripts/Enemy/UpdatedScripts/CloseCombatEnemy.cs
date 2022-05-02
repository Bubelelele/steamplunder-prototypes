using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    public override void EnemyInSight()
    {
        Debug.Log("Close");
    }
    public override void EnemyOutOfSight()
    {
        Debug.Log("NotClose");
    }
}
