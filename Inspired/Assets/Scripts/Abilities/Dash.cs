using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/DashAbility")]
public class Dash : Ability
{
    [SerializeField] float dashVelocity;

    Vector2 dashDirection;
    Vector2 lastLookedPosition;
    public override void Activate(GameObject parentSent)
    {
        parent = parentSent;
        player = parentSent.GetComponent<PlayerController>();
        dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastLookedPosition = dashDirection - (Vector2)parentSent.transform.position;
        lastLookedPosition = lastLookedPosition.normalized;
        parent.transform.up = lastLookedPosition;
        player.SetStateToAbilityInUse();
    }
    public override void AbilityActive()
    {
        if (parent == null) return;
        Debug.Log("ActivateAbility");
        float step = dashVelocity * Time.deltaTime;
        parent.transform.position += parent.transform.up * step;
        //parent.transform.position = Vector2.MoveTowards(parent.transform.position, dashDirection, step);

    }
    public override void DeActivateAbility()
    {
        player.SetStateToNormal();
    }
}
