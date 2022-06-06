using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public State state;

    [SerializeField] float speed = 20f;

    [SerializeField] Transform thrusterEffect;
    Vector3 randomDirection;
    [SerializeField] Transform firePoint1, firePoint2;
    [SerializeField] Transform laserPrefab;

    float range = 30f;
    float currentRange;
    float bonusRange;
    float shootingRate = 1f;
    float currentShootingRate;
    float bonusShootingRate;
    float shotTimer = 1f;
    Transform currentTarget = null;
    float distanceBetweenPlayerAndTarget;
    [SerializeField] float baseAttackDamage = 10f;
    float currentAttackDamage;
    float bonusAttackDamage;
    float visionRange = 40f;
    public enum State
    {
        Roaming,
        ShootableTargeted,
        PlayerWithinRange
    }
    // Start is called before the first frame update
    void Start()
    {
        randomDirection = Random.insideUnitCircle.normalized;
        state = State.Roaming;
        currentShootingRate = shootingRate;
        shotTimer = shootingRate;
        currentAttackDamage = baseAttackDamage;
    }

    void Update()
    {
        switch (state)
        {
            case State.Roaming:
                HandleMovement();
                ScanForItemsWithinRange();
                HandleReload();
                break;
            case State.ShootableTargeted:
                HandleGoingToAndShootingTarget();
                HandleReload();
                break;
        }
    }

    void HandleMovement()
    {
        float step = speed * Time.deltaTime;
        transform.position += randomDirection * step;

        transform.up = randomDirection;
        thrusterEffect.gameObject.SetActive(true);
    }

    void ScanForItemsWithinRange()
    {
        Debug.Log(state);
        float currentClosestObjectMag = 0f;
        Transform currentClosestObject = null;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, visionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.GetComponent<Shootable>())
            {
                if (currentClosestObject == null)
                {
                    currentClosestObjectMag = (hitCollider.transform.position - this.transform.position).magnitude;
                    currentClosestObject = hitCollider.transform;
                }
                if ((hitCollider.transform.position - this.transform.position).magnitude < currentClosestObjectMag)
                {
                    currentClosestObjectMag = (hitCollider.transform.position - this.transform.position).magnitude;
                    currentClosestObject = hitCollider.transform;
                }
                Debug.Log(hitColliders.Length);
                if (currentClosestObject != null)
                {
                    SetTarget(currentClosestObject);
                }
            }
        }
        
    }
    void SetTarget(Transform targetSent)
    {
        this.currentTarget = targetSent;
        state = State.ShootableTargeted;
    }
    void HandleGoingToAndShootingTarget()
    {

        if (currentTarget == null)
        {
            return;
        }
        Debug.Log(currentTarget);
        transform.up = currentTarget.position - transform.position;
        distanceBetweenPlayerAndTarget = (currentTarget.position - transform.position).magnitude;
        if (distanceBetweenPlayerAndTarget > range)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
        }
        else
        {
            //we are within range fire a shot

            HandleShoot();
            //handleshootlogic
        }
    }
    void HandleReload()
    {
        shotTimer += Time.deltaTime;
    }
    void HandleShoot()
    {
        if (shotTimer > currentShootingRate)
        {
            shotTimer = 0f;
            Shoot();

        }
    }

    void Shoot()
    {
        Transform laser1 = Instantiate(laserPrefab, firePoint1.position, this.transform.rotation);
        laser1.GetComponent<LaserBehaviour>().SetTarget(currentTarget, currentAttackDamage);
        Transform laser2 = Instantiate(laserPrefab, firePoint2.position, this.transform.rotation);
        laser2.GetComponent<LaserBehaviour>().SetTarget(currentTarget, currentAttackDamage);
    }
}
