using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public int platformCount = 8;
    public Vector2 playerSpawnPoint;

    public int minDistance = 8;
    public int maxXDistance = 40;
    public int maxYDistance = 15;

    private Vector2[] spawnPoints;
    public GameObject platformPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Vector2[platformCount];

        Vector2 currentXInterval = new Vector2(playerSpawnPoint.x - 12f, playerSpawnPoint.x + 12f);
        Vector2 currentYInterval = new Vector2(playerSpawnPoint.y + 1f, playerSpawnPoint.y + 3f);

        for(int i = 0; i < platformCount; i++)
        {
            float x = Random.Range(currentXInterval.x, currentXInterval.y);
            float y = Random.Range(currentYInterval.x, currentYInterval.y);

            spawnPoints[i] = new Vector2(x, y);

            if (i > 0)
            {
                while(Vector2.Distance(spawnPoints[i], spawnPoints[i - 1]) < minDistance)
                {
                    x = Random.Range(currentXInterval.x, currentXInterval.y);
                    y = Random.Range(currentYInterval.x, currentYInterval.y);
                    spawnPoints[i] = new Vector2(x, y);
                }
            }

            currentXInterval.x = x - 10f;
            currentXInterval.y = Mathf.Clamp(x + 10f, -maxXDistance, maxXDistance);


            currentYInterval.x = y + 2.0f;
            currentYInterval.y = Mathf.Clamp(y + 4f, currentYInterval.x, currentYInterval.x + maxYDistance);

            
            Instantiate(platformPrefab, spawnPoints[i], Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
