using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapScript : MonoBehaviour
{
    bool loaded = true;
    public GameObject arrow;
    public Vector2 arrowSpeed = new Vector2(10, 0);
    GameObject arrow_1;
    GameObject arrow_2;
    public Animator animator;
    
    void Update()
    {
        Watch();
    }

    void Watch()
    {
        if(loaded)
        {
            Vector2 startPos = new Vector2(transform.position.x + 1.01f*transform.localScale.x, transform.position.y); 
            RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.right*transform.localScale.x,8f);
            Debug.DrawRay(startPos, Vector3.right*transform.localScale.x*8, Color.red, 0.1f);
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Vector2 arrow_1_SpawnPosition = new Vector2(this.transform.position.x + 0.5f*transform.localScale.x, this.transform.position.y + 0.15f);
                    Vector2 arrow_2_SpawnPosition = new Vector2(this.transform.position.x + 0.5f*transform.localScale.x, this.transform.position.y - 0.15f);
                    if(transform.localScale.x > 0)
                    {
                        arrow_1 = Instantiate(arrow, arrow_1_SpawnPosition,transform.rotation);
                        arrow_2 = Instantiate(arrow, arrow_2_SpawnPosition, transform.rotation); 
                    }
                    else
                    {
                        arrow_1 = Instantiate(arrow, arrow_1_SpawnPosition, Quaternion.AngleAxis(180,Vector3.back));
                        arrow_2 = Instantiate(arrow, arrow_2_SpawnPosition, Quaternion.AngleAxis(180,Vector3.back));
                    }

                    if(arrow_1 != null && arrow_2 != null)
                    {
                        arrow_1.GetComponent<Rigidbody2D>().velocity = arrowSpeed;
                        arrow_2.GetComponent<Rigidbody2D>().velocity = arrowSpeed;
                        loaded = false;
                        animator.Play("Empty_Arrow_Trap");
                    }
                    
                }
            }
        }
        
    }
  
}
