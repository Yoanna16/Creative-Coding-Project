using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGeneration : MonoBehaviour
{
    // stores the Level / stores if each tile is a wall(true) or a path(false)
    bool[,] map;

    //how much of the map should be filled
    [Range(0,1)]
    public float fillPercentage;

    //map Size
    public int width = 100;
    public int height = 100;

    //how many times the map should be smoothed. More smoothing results in larger and less random caves
    public int smoothIterations = 5;

    //tiles which represent the walls
    public GameObject wallPrefab;
    List<GameObject> walls;

    void Start()
    {
        map = new bool[width, height];
        walls = new List<GameObject>();

        generateMap();
    }

    void generateMap()
    {
        initializeMap();

        for(int i = 0; i < smoothIterations; i++)
        {
            smoothMap();
        }

        drawMap();
    }

    //destroy current map and generate new one
    void regenerateMap()
    {
        foreach (GameObject tile in walls)
        {
            Destroy(tile);
        }

        walls = new List<GameObject>();

        generateMap();
    }

    //generate completely random initial map, which is filled according to fillPercentage
    void initializeMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.Range(0f, 1f) <= fillPercentage)
                {
                    map[x, y] = true;
                }
                else
                {
                    map[x, y] = false;
                }
            }
        }
    }

    //draw the map / create the gameobjects which represent the walls
    void drawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y])
                {
                    Vector3 position = new Vector3(x, y, 0);

                    //also store the generated walls so they can be destroyed later 
                    walls.Add(Instantiate<GameObject>(wallPrefab, position, Quaternion.identity));
                }
            }
        }
    }

    //smooth map (cellular automaton)
    void smoothMap()
    {
        int[,] neighborMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighborMap[x, y] = getCellNeighbors(x, y);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(neighborMap[x, y] < 4)
                {
                    map[x, y] = false;
                }
                else
                {
                    map[x, y] = true;
                }
            }
        }
    }

    //count neighbors of a given cell, which are walls
    int getCellNeighbors(int x, int y)
    {
        int neighborCount = 0;

        //if a cell is on an edge, it is regarded as having 8 neighbors, causing it to become a wall
        if (x == 0 || y == 0 || x == width - 1 || y == height - 1) 
        {
            return 8;
        }

        if (map[x - 1, y - 1]) { neighborCount++; }
        if (map[x,     y - 1]) { neighborCount++; }
        if (map[x + 1, y - 1]) { neighborCount++; }
        if (map[x - 1, y    ]) { neighborCount++; }
        if (map[x + 1, y    ]) { neighborCount++; }
        if (map[x - 1, y + 1]) { neighborCount++; }
        if (map[x,     y + 1]) { neighborCount++; }
        if (map[x + 1, y + 1]) { neighborCount++; }

        return neighborCount;
    }

    void Update()
    {
        //press R to get a different map
        if (Input.GetKeyDown(KeyCode.R))
        {
            regenerateMap();
        }
    }
}
