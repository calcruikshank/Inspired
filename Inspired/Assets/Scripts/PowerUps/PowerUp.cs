using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]PowerUpEffect[] potentialPowerUps;
    PowerUpEffect powerUpEffect;


    private void Start()
    {
        if (potentialPowerUps.Length >= 1)
        {
            powerUpEffect = potentialPowerUps[Random.Range(0, potentialPowerUps.Length)];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Stats>() != null)
        {
            powerUpEffect.Apply(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
