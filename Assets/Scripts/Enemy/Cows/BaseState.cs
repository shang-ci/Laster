
public abstract class BaseState 
{
    protected EnemyAI currentEnemy;
    public abstract void OnEnter(EnemyAI enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
