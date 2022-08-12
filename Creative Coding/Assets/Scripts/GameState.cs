using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    int score = 0;

    public GameObject scoreUIobject;
    TextMeshProUGUI scoreUI;

    public GameObject gameOverUIObject;

    public static Transform gameState;

    public Transform player;

    bool[,] map;
    int width;
    int height;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI = scoreUIobject.GetComponent<TextMeshProUGUI>();
        gameState = transform;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
        }
    }

    public void updateScore()
    {
        Debug.Log("updated score");
        score++;
        scoreUI.SetText("Score: " + score);
    }

    public void spawnPlayer()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        if (map[x, y])
        {
            spawnPlayer();
        }
        else
        {
            player.position = new Vector3(x - width / 2, 0, y - height / 2);
        }
    }

    public void gameOver()
    {
        Debug.Log("Game over");

        gameOverUIObject.SetActive(true);

        score = 0;
        scoreUI.SetText("Score: " + score);

        Time.timeScale = 0;
    }

    public void restart()
    {
        score = 0;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
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
