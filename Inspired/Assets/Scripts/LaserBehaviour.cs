using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    Transform currentTarget = null;
    float speed = 100f;
    float damageOfProj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            if (transform.position != currentTarget.position)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
                transform.up = currentTarget.position - transform.position;
            }

            else
            {
                currentTarget.GetComponent<Shootable>().TakeDamage(damageOfProj);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Transform sentTarget, float damageSent)
    {
        currentTarget = sentTarget; damageOfProj = damageSent;
    }
}
