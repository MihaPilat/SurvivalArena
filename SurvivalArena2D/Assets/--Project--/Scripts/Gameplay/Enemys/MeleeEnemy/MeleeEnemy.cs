using System.Collections.Generic;

public class MeleeEnemyEntity : EnemyEntity
{
    private MeleeChaseBehaviour _chaseBehaviour = new MeleeChaseBehaviour();
    protected override List<IState> AddStates()
    {
        return new List<IState>()
        {
            new ChaseState(this,_chaseBehaviour),
            new IdleState(this),
            new DeathState(this)
        };
    }
}
