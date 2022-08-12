using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public float timeToNewEnemy = 1.0f;

    public GameObject enemy;

    public static int enemyCount = 0;
    public int maxEnemies = 100;

    float timer;

    bool[,] map;
    int width;
    int height;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeToNewEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            if(enemyCount < maxEnemies)
            {
                spawnEnemy();
            }
            timer = timeToNewEnemy;
        }
        timer -= Time.deltaTime;
    }

    void spawnEnemy()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        if (map[x, y])
        {
            spawnEnemy();
        }
        else
        {
            Vector3 position = new Vector3(x - width / 2, 0, y - height / 2);
            Instantiate<GameObject>(enemy, position, Quaternion.identity);

            enemyCount++;
        }
    }

    public void setMap(bool[,] map)
    {
        this.map = map;
    }

    public void setDimensions(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}
