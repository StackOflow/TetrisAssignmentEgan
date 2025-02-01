using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Grid Height & Width, public in case we want to edit later
    public int width = 10;
    public int height = 20;

    //This transform will be the actual grid of the tetris game
    public AudioSource audioSource;
    private Transform[,] grid;
    public AudioClip[] celebrations;
    public float dannyTimer = 0;
    public GameObject dannyDevito;

    void Start()
    {
        //create the grid using the width and height variables
        grid = new Transform[width, height];
        dannyDevito.SetActive(true);


    }
    //Check if cell is occupied 
    public bool IsCellOccupied(Vector2Int position, bool gameOverCheck = false)
    {
        if (position.x < 0 || position.x >= width || position.y >= height || position.y <= 0)
        {
            if (gameOverCheck)
            {
                return false;
            }
            return true; //Out of Bounds
        }
        //Returning the result of the statement, if it's occupied it will return true, of not return false
        return grid[position.x, position.y] != null; // True if there is an object at our position
    }


    public void AddtoGrid(Transform piece)
    {
        foreach (Transform block in piece)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);
            grid[position.x, position.y] = block;
        }
    }

    public bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public void ClearLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void ShiftRowsDown(int clearedRow)
    {
        for (int y = clearedRow; y < height - 1; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = grid[x, y + 1];
                if (grid[x, y] != null)
                {
                    grid[x, y].position += Vector3.down;
                }
                grid[x, y + 1] = null;
            }
        }
    }

    public void PlayCelebAudio()
    {
        int index = UnityEngine.Random.Range(0, 5);
        audioSource.clip = celebrations[index];
        audioSource.Play();
    }

   

    public void ClearFullLines()
    {
        int linesCleared = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                linesCleared++;
                ClearLine(y);
                ShiftRowsDown(y);
                y--;

                dannyTimer = 7;
                PlayCelebAudio();
                
                
                
                
                
            }
        }

        FindAnyObjectByType<ScoreManager>().AddScore(linesCleared);
    }

    public void Update()
    {
        dannyTimer -= (1 * Time.deltaTime);

        if (dannyTimer >= 0)
        {
            dannyDevito.SetActive(true);
            
        }
        else
        {
            dannyDevito.SetActive(false);
        }
    }


}
