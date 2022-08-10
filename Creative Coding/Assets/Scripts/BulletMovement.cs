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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(gameObject);
    }
}
