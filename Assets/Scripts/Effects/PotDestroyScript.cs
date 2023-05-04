using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotDestroyScript : MonoBehaviour
{
     // Start is called before the first frame update
    public GameObject brokenPot;
    public GameObject item;
    public GameObject potParticles;
    //public GameObject pickupLaser;

    public void setItem(GameObject newItem)
    {
        //Placeholder, update when more items to put in crate
        item = newItem;
    }

    public void destroyPot()
    {
        if(item != null)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
        
        GameObject instance = Instantiate(brokenPot, transform.position, transform.rotation);
        instance.GetComponent<CorpseExplode>().explode();
        Vector2 particlePos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Instantiate(potParticles, particlePos, transform.rotation);
        //Vector2 laserPos = new Vector2(transform.position.x, transform.position.y + 0.3f);
        //Instantiate(pickupLaser, laserPos, transform.rotation);
        Destroy(gameObject);
    }
}
