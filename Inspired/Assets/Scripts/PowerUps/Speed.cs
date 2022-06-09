using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class Speed : PowerUpEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<Stats>().moveSpeed < 50)
        {
            target.GetComponent<Stats>().moveSpeed += amount;
        }
    }
}
