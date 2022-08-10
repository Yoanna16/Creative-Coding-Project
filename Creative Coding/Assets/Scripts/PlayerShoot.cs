using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;

    public Transform mousePoint;

    public float bulletSpeed;

    private GameObject currentBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawnBullet();
        }
    }

    void spawnBullet()
    {
        Vector3 bulletVelocity = mousePoint.position - transform.position;
        bulletVelocity.Normalize();
        bulletVelocity = bulletVelocity * bulletSpeed;
        bulletVelocity.y = 0;

        currentBullet = Instantiate<GameObject>(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<BulletMovement>().velocity = bulletVelocity;
    }
}
