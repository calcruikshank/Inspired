using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    [SerializeField] Transform[] potentialBasePowerUps;
    public void InstantiateRandomPowerUps()
    {
        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            float randomX = Random.Range(this.transform.position.x - this.GetComponent<Collider2D>().bounds.size.x / 2, this.transform.position.x + this.GetComponent<Collider2D>().bounds.size.x / 2);
            float randomY = Random.Range(this.transform.position.y - this.GetComponent<Collider2D>().bounds.size.y / 2, this.transform.position.y  + this.GetComponent<Collider2D>().bounds.size.y / 2);

            Vector2 randomPositionWithinBounds = new Vector2(randomX, randomY);
            Transform newPowerUp = Instantiate(potentialBasePowerUps[Random.Range(0, potentialBasePowerUps.Length)], randomPositionWithinBounds, Quaternion.identity);
        }
    }
}
