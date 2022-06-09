using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform thrusterEffect;

    Vector2 lastClickedPosition;
    Vector2 lastLookPosition;

    [SerializeField] Transform firePoint1, firePoint2;
    [SerializeField] Transform laserPrefab;

    Stats stats;

    Transform currentTarget = null;
    float distanceBetweenPlayerAndTarget;

    float shotTimer;
    public State state;
    public enum State
    {
        Normal,
        ShootableTargeted,
        AbilityInUse
    }


    private void Awake()
    {
        stats = this.GetComponent<Stats>();
        shotTimer = stats.fireRate;
    }
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                HandleInput();
                HandleMoving();
                HandleReload();
                break;
            case State.ShootableTargeted:
                HandleInput();
                HandleGoingToAndShootingTarget();
                HandleReload();
                break;
            case State.AbilityInUse:
                HandleAbility();
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
            float step = stats.moveSpeed * Time.deltaTime;
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
        //just add a little delay to each shot 
       

        if (currentTarget == null)
        {
            return;
        }
        transform.up = currentTarget.position - transform.position;
        distanceBetweenPlayerAndTarget = (currentTarget.position - transform.position).magnitude;
        if (distanceBetweenPlayerAndTarget > stats.range)
        {
            float step = stats.moveSpeed * Time.deltaTime;
            if (shotTimer > .2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
            }
        }
        else
        {
            HandleShoot();
            //handleshotlogic
        }
    }
    void HandleReload()
    {
        shotTimer += Time.deltaTime;
    }
    void HandleShoot()
    {
        if (shotTimer > stats.fireRate)
        {
            shotTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        Transform laser1 = Instantiate(laserPrefab, firePoint1.position, this.transform.rotation);
        laser1.GetComponent<LaserBehaviour>().SetTarget(currentTarget, stats.attackPower);
        Transform laser2 = Instantiate(laserPrefab, firePoint2.position, this.transform.rotation);
        laser2.GetComponent<LaserBehaviour>().SetTarget(currentTarget, stats.attackPower);
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

    void HandleAbility()
    {

    }
    void SetTarget(Transform targetSent)
    {
        this.currentTarget = targetSent;
        state = State.ShootableTargeted;
    }

    public void SetStateToNormal()
    {
        lastClickedPosition = transform.position;
        currentTarget = null;
        state = State.Normal;
    }


    public void SetStateToAbilityInUse()
    {
        state = State.AbilityInUse;
    }

    public void TakeDamage(float damageTaken)
    {
        stats.currentHealth -= damageTaken;
    }
}
