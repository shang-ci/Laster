using UnityEngine;

public class PatroState : BaseState
{ 
    public override void OnEnter(EnemyAI enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.animator.SetInteger("State", 1);
    }

    public override void LogicUpdate()
    {

        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwichState(EnemiesState.Chase);
        }

        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall && currentEnemy.fixDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.fixDir.x > 0)
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetInteger("State", 0);
        }
        else
        {
            currentEnemy.animator.SetInteger("State", 1);
        }
    }


    public override void PhysicsUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        
    }
}
