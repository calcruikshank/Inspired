using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    protected float startingHealth = 60f;
    protected float health = 60f;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damageTaken)
    {
       
    }



}
