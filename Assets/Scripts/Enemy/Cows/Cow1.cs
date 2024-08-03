using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow1 : EnemyAI
{
    public override void Awake()
    {
        base.Awake();
        patrolState = new PatroState();
        chaseState = new ChaseState();
    }
}
