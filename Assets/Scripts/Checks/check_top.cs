using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_top : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelGeneration LevelGenerator;
    public bool doneCheck = false;
    public Sprite dirt;
    public bool isHit;
    public bool checkTopFull;

    public GameObject[] grasses;
    public GameObject[] bushes;

    //private RaycastHit2D hit;
    void Start()
    {
        LevelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>();
        //Vector2 rayPosition = new Vector2(transform.position.x, transform.position.y + 0.51f);
        //hit = Physics2D.Raycast(rayPosition, transform.TransformDirection(Vector2.up), 0.01f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(!doneCheck && LevelGenerator.finishedFill)
        {
            Vector2 rayPosition = new Vector2(transform.position.x, transform.position.y + 0.51f);
            RaycastHit2D hit = Physics2D.Raycast(rayPosition, transform.TransformDirection(Vector2.up), 0.01f);
            //Vector2 point2 = new Vector2(rayPosition.x, rayPosition.y +0.1f);
            //RaycastHit2D hit = Physics2D.Linecast(rayPosition, point2);
            
            //Debug.Log(LevelGenerator.playerSpawned);
            //isHit = hit;
            if(hit && checkTopFull)
            {
                //removes grass
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = dirt;
                //Debug.Log("checkedTop");

            }
            if(!hit && !checkTopFull)
            {
                //adds grass
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = dirt;
                GenerateFoliage();
            }
            if(!hit && checkTopFull)
            {
                //is grass
                GenerateFoliage();
            }
            doneCheck = true;

        }
    }

    void GenerateFoliage()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + 1);
        //bush or grass or nothing
        int bushOrGrass = Random.Range(0, 7);
        if(bushOrGrass < 4)
        {
            //Grass
            int rand = Random.Range(0, grasses.Length);
            Instantiate(grasses[rand], newPos, transform.rotation);
        }
        else if(bushOrGrass == 5)
        {
            int rand = Random.Range(0, bushes.Length);
            Instantiate(bushes[rand], newPos, transform.rotation);
        }
    }


}
