using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    CharacterController controller;

    public static Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 motion = new Vector3(x, 0, y);
        controller.Move(motion * movementSpeed * Time.deltaTime);

        flipCharacter(motion.x);
    }

    void flipCharacter(float xMotion)
    {
        Vector3 scale = new Vector3(1, 1, 1);

        if (xMotion >= 0)
        {
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }

        transform.localScale = scale;
    }
}
