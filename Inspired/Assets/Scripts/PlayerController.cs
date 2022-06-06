using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Transform thrusterEffect;

    Vector2 lastClickedPosition;
    Vector2 lastLookPosition;

    [SerializeField] Transform firePoint1, firePoint2;
    [SerializeField] Transform laserPrefab;

    [SerializeField] float baseAttackDamage = 10f;
    float currentAttackDamage;
    float bonusAttackDamage;

    float range = 30f;
    float currentRange;
    float bonusRange;

    [SerializeField] float shootingRate = 10f;
    float currentShootingRate;
    float bonusShootingRate;

    Transform currentTarget = null;

    public State state;
    public enum State
    {
        Normal,
        ShootableTargeted
    }

    void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleInput();
                HandleMoving();
                break;
            case State.ShootableTargeted:
                HandleInput();
                HandleGoingToAndShootingTarget();
                break;
        }
    }


    void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                FixedHandleMovement();
                break;
        }
    }

    private void HandleMoving()
    {
        if ((Vector2)transform.position != lastClickedPosition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPosition, step);

            transform.up = lastLookPosition;
            thrusterEffect.gameObject.SetActive(true);
        }
        else
        {
            thrusterEffect.gameObject.SetActive(false);
        }
        
    }

    private void FixedHandleMovement()
    {
    }

    void HandleGoingToAndShootingTarget()
    {
        float distanceBetweenPlayerAndTarget = (currentTarget.position - transform.position).magnitude;
        Debug.Log(distanceBetweenPlayerAndTarget);
        if (distanceBetweenPlayerAndTarget > range)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
            transform.up = currentTarget.position - transform.position;
        }
        else
        {
            //we are within range fire a shot

            Shoot();
            //handleshootlogic
        }
    }

    void Shoot()
    {

    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.transform.GetComponent<Shootable>())
                {
                    Shootable shootable = hit.collider.transform.GetComponent<Shootable>();
                    Debug.Log("Shootable hit" + shootable.transform);

                    SetTarget(shootable.transform);
                }
                if (hit.collider.transform.GetComponent<PlayerController>() && hit.collider.transform != this.transform)
                {
                }
                if (hit.collider.transform.GetComponent<AIEnemy>())
                {
                }
                
            }
            else
            {
                SetStateToNormal();
            }
        }
        if (Input.GetMouseButton(1))
        {
            lastClickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastLookPosition = lastClickedPosition - (Vector2)transform.position;
        }

        
    }

    void SetTarget(Transform targetSent)
    {
        this.currentTarget = targetSent;
        state = State.ShootableTargeted;
    }

    void SetStateToNormal()
    {
        currentTarget = null;
        state = State.Normal;
    }
}
