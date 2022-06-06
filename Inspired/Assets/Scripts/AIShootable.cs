using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootable : Shootable
{
    AIEnemy aiEnemy;
    private void Awake()
    {
        aiEnemy = this.GetComponent<AIEnemy>();
    }

    public override void TakeDamage(float damageTaken)
    {
        aiEnemy.TakeDamage(damageTaken);
    }
}
