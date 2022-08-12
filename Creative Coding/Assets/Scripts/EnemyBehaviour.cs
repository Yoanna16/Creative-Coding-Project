using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Vector3 playerDirection;

    public float movementSpeed;

    public float killDistance = 1f;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        seePlayer();

        if (seePlayer())
        {
            approachPlayer();
        }
        
        killPlayer();
    }

    bool seePlayer()
    {
        RaycastHit hit;

        playerDirection = PlayerMovement.playerTransform.position - transform.position;

        if(Physics.Raycast(transform.position, playerDirection, out hit, 100))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }

    void approachPlayer()
    {
        Vector3 motion = playerDirection.normalized * movementSpeed * Time.deltaTime;
        motion.y = 0;

        controller.Move(motion);

        flipCharacter(motion.x);
    }

    void flipCharacter(float xMotion)
    {
        Vector3 scale = new Vector3(1, 1, 1);

        if (xMotion >= 0)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }

        transform.localScale = scale;
    }

    void killPlayer()
    {
        if(Vector3.Distance(PlayerMovement.playerTransform.position, transform.position) < killDistance)
        {
            GameState.gameState.GetComponent<GameState>().gameOver();
        }
    }
}
