using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs;
    private Grid grid;
    private GameObject currentPiece;
    private GameObject nextPiece;
    public GameObject previewPanel;
    public GameObject gameOverScreen;
    

    void Start()
    {
        grid = FindAnyObjectByType<Grid>();
        SpawnPiece();
    }




    public void SpawnPiece()
    {
        Vector3 spawnPosition = new Vector3(Mathf.Floor(grid.width / 2f), grid.height - 1, 0);

        if (nextPiece != null)
        {
            currentPiece = nextPiece;
            currentPiece.SetActive(true);
            currentPiece.GetComponent<Piece>().enabled = true;
            currentPiece.transform.position = spawnPosition;
        }

        else
        {
            currentPiece = InstantiateRandomPiece();
            currentPiece.transform.position = spawnPosition;
        }

        
        nextPiece = InstantiateRandomPiece();
        nextPiece.GetComponent<Piece>().enabled = false;
        nextPiece.transform.position = previewPanel.transform.position;

        if (IsGameOver())
        {
            return;
        }
    }

    private GameObject InstantiateRandomPiece()
    {
        int Index = Random.Range(0, tetrominoPrefabs.Length);
        return Instantiate(tetrominoPrefabs[Index]);
    }

    private bool IsGameOver()
    {
        foreach(Transform block in currentPiece.transform)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);
            if (grid.IsCellOccupied(position))
            {
                gameOverScreen.SetActive(true);
                return true;
            }

            
        }
        return false;
    }

}
