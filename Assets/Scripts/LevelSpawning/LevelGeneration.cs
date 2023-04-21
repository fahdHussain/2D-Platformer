using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public float moveDistance = 10;
    public Transform[] startingPositions;
    public GameObject[] rooms;
    private int direction;

    //rooms[0] == LR, rooms[1] == LRB, rooms[2] == LRT, rooms[3] == LRTB
    private int room;
    public float startTimeBtwnRooms = 0.25f;
    private float timeBtwnRooms;
    public int startingPos;

    public float minX = -5;
    public float maxX = 25;
    public float minY = -25;
    private bool stopGeneration;
    public LayerMask roomMask;
    private int downCounter = 0;

    private int[,] levelArray = new int[4,4];
    private int levelArray_row = 0;
    private int levelArray_col;
    public bool finishedFill = false;
    public bool playerSpawned;

    private int finalRoom_row;
    private int finalRoom_col;

    void Start()
    {
        startingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[startingPos].position;

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1,6);

        levelArray[0,startingPos] = 1;
        levelArray_col = startingPos;
    }

    void move()
    {
        
        if(direction == 1 || direction == 2) // move right
        {
            
            if(transform.position.x + moveDistance < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                levelArray_col += 1;
                levelArray[levelArray_row, levelArray_col] = 1;

                direction = Random.Range(1, 6);
                if(direction == 3)
                {
                    direction = 1;
                } else if(direction == 4)
                {
                    direction = 5;
                }

            } else
            {
                direction = 5;
            }
        }
        else if(direction == 3 || direction == 4) // move left
        {
            
            if(transform.position.x - moveDistance > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveDistance, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                levelArray_col -= 1;
                levelArray[levelArray_row, levelArray_col] = 1;

                direction = Random.Range(3,6);
            } else
            {
                direction = 5;
            }
        }
        else if(direction == 5) // move down
        {
            if(transform.position.y - moveDistance > minY)
            {
                downCounter ++;

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position,1, roomMask);
                if(roomDetection.GetComponent<RoomType>().type == 0 || roomDetection.GetComponent<RoomType>().type == 2)
                {

                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().destroyRoom();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().destroyRoom();

                        int randBool = Random.Range(0,2);
                        if(randBool == 0)
                        {
                            Instantiate(rooms[1], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(rooms[3], transform.position, Quaternion.identity);
                        }
                    }                  
                }
           
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveDistance);
                transform.position = newPos;
                direction = Random.Range(1,6);

                int randRoom = Random.Range(2,4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                levelArray_row += 1;
                levelArray[levelArray_row, levelArray_col] = 1;

            } else {
                stopGeneration = true;
            }
        }       
        
    }

    void fillLevel()
    {
        transform.position = startingPositions[0].position;

        for(int i = 0; i <= 3; i++)
        {
            for(int j = 0; j <= 3; j++)
            {   

                if(levelArray[i,j] == 0)
                {
                    int randRoom = Random.Range(0,4);
                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                }

                Vector2 newPos = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.position = newPos;
            }

            Vector2 nextRow = new Vector2(startingPositions[0].position.x, transform.position.y - moveDistance);
            transform.position = nextRow;
        }
        finishedFill = true;
    }
    
    
    void Update()
    {
        timeBtwnRooms += Time.deltaTime;
        if(timeBtwnRooms > startTimeBtwnRooms && !stopGeneration)
        {
            move();
            timeBtwnRooms = 0;
            //Debug.Log("Row: "+levelArray_row.ToString()+" Col: "+levelArray_col);
        }

        if(stopGeneration && !finishedFill)
        {
            fillLevel();
            finishedFill = true;
            //Debug.Log("FinishedFill");

            transform.position = startingPositions[startingPos].position;
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomMask);

            roomDetection.GetComponent<RoomType>().spawnPlayer();
        }
    }

}
