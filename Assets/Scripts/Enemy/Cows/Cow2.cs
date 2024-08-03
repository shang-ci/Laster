using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow2 : EnemyAI
{

    public override void Awake()
    {
        base.Awake();
        patrolState = new PatroState();
        chaseState = new ChaseState();
    }
}

