using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/FireRateBuff")]
public class FireRate : PowerUpEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<Stats>().fireRate > .1f)
        {
            target.GetComponent<Stats>().fireRate -= amount;
            if (target.GetComponent<Stats>().fireRate < .1f)
            {
                target.GetComponent<Stats>().fireRate = .1f;
            }
        }
    }
}
