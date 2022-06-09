using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public static AISpawner singleton;

    [SerializeField] GameObject aiEnemyToSpawn;
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            float spawnY = Random.Range
                (-280, 280);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
            float spawnX = Random.Range
                (-280, 280);
            //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            GameObject newEnemy = Instantiate(aiEnemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SpawnRandomEnemy()
    {
        float spawnY = Random.Range
               (-280, 280);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) * 8;
        float spawnX = Random.Range
            (-280, 280);
        //(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x) * 8;

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject newEnemy = Instantiate(aiEnemyToSpawn, spawnPosition, Quaternion.identity);
        
    }
}
