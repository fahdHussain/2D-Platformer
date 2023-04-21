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
    void Start()
    {
        LevelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 rayPosition = new Vector2(transform.position.x, transform.position.y + 0.6f);
        //Debug.DrawRay(rayPosition,transform.TransformDirection(Vector2.up), Color.red);
        //Debug.Log(LevelGenerator.finishedFill);

        if(!LevelGenerator.playerSpawned && LevelGenerator.finishedFill)
        {
            Vector2 rayPosition = new Vector2(transform.position.x, transform.position.y + 0.51f);
            //RaycastHit2D hit = Physics2D.Raycast(rayPosition, transform.TransformDirection(Vector2.up), 0.01f);
            Vector2 point2 = new Vector2(rayPosition.x, rayPosition.y +0.1f);
            RaycastHit2D hit = Physics2D.Linecast(rayPosition, point2);
            
            //isHit = hit;
            //Debug.Log("checkedTop");
            if(hit && checkTopFull)
            {
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = dirt;
                //Debug.Log("checkedTop");
            }
            if(!hit && !checkTopFull)
            {
                SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = dirt;
            }
            //doneCheck = true;

        }
    }
}
