using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    Transform currentTarget = null;
    float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
            transform.up = currentTarget.position - transform.position;
        }
    }

    public void SetTarget(Transform sentTarget)
    {
        currentTarget = sentTarget;
    }
}
