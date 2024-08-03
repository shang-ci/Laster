using UnityEngine;

public class ChaseState : BaseState
{
    public override void OnEnter(EnemyAI enemy)
    {
        currentEnemy = enemy;
        Debug.Log("zhudao");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetInteger("State", 2);
    }
    
    public override void LogicUpdate()
    {
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall && currentEnemy.fixDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.fixDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.fixDir.x,1,1);
        }
    }

     public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

   
}
