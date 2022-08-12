using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Vector3 velocity;

    public float destructionTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        destroyBullet();

        flipBullet();
    }

    void flipBullet() {
        Vector3 scale = new Vector3(1, 1, 1);

        if (velocity.x >= 0)
        {
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }

        transform.localScale = scale;
    }

    void destroyBullet()
    {
        if (destructionTime < 0)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }

        destructionTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            SpawnEnemies.enemyCount--;

            GameState.gameState.GetComponent<GameState>().updateScore();
        }
        
    }
}
