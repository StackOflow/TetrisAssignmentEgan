using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Piece : MonoBehaviour
{

    private Grid grid;
    [SerializeField]
    private float dropInterval = 1.0f; //time between automatic drops
    private float dropTimer; 
    private TetrisSpawner spawner;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
        dropTimer = dropInterval;
    }

    void Update()
    {
        HandleInput();
        HandleAutomaticDrop();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            Move(Vector3.left);

        if (Input.GetKeyDown(KeyCode.RightArrow)) 
            Move(Vector3.right);

        if ( Input.GetKeyDown(KeyCode.DownArrow)) 
            Move(Vector3.down);

        if (Input.GetKeyDown(KeyCode.Space)) 
            RotatePiece();
    }

    private void HandleAutomaticDrop()
    {
        dropTimer -= Time.deltaTime;
        if (dropTimer <= 0) //If timer is less than/equal too zero, move piece down and reset
        {
            Move(Vector3.down);
            dropTimer = dropInterval; //Reset the timer
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction; // Move our piece
        
        // if valid position
        if (!IsValidPosition()) //check if piece is allowed to be there
        {
            transform.position -= direction; //revert if it is not allowed

            if (direction == Vector3.down)
            {
                LockPiece();
            }
        }
    }

    private bool IsValidPosition()
    {
        foreach (Transform block in transform)
        {
            Vector2Int position = Vector2Int.RoundToInt(block.position);

            if (grid.IsCellOccupied(position))
                return false;
        }

        return true;
    }
    
    private void RotatePiece()
    {
        transform.Rotate(0, 0, 90); //Rotate our piece

        if (!IsValidPosition()) //Check if the piece is allowed to be there
            transform.Rotate(0, 0, -90); //Revert if it is not allowed
    }

    private void LockPiece()
    {
        grid.AddtoGrid(transform); //putt piece on grid
        grid.ClearFullLines(); //check and clear lines
        FindObjectOfType<TetrisSpawner>().SpawnPiece(); //give new piece
        Destroy(this); //get rid of this script
    }

}
