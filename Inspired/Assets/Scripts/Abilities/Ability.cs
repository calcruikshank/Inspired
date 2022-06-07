using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    protected GameObject parent;
    protected PlayerController player;
    public virtual void AttachToPlayer(GameObject parentSent)
    {

    }
    public virtual void Activate(GameObject parentSent)
    {

    }
    public virtual void AbilityActive()
    {
    }
    public virtual void DeActivateAbility()
    {
    }
}
