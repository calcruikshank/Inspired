using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootable : Shootable
{
    PlayerController player;

    private void Awake()
    {
        this.player = this.GetComponent<PlayerController>();
    }
    public override void TakeDamage(float damageTaken)
    {
        player.TakeDamage(damageTaken);
    }
}
