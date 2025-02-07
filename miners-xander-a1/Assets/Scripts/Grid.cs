using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Grid Width and Height
    public int width = 10;
    public int height = 20;

    //This transform makes up the grid itself
    private Transform[,] grid;

    void Awake()
    {
        //create the grid using the width and height variables
        grid = new Transform[width, height];


    }
    //Check if a cell is occupied 
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
        //Returning the result of the statement, occupied = true
        return grid[position.x, position.y] != null;
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
                
                
                
                
                
            }
        }

        FindAnyObjectByType<ScoreManager>().AddScore(linesCleared);
    }


}
