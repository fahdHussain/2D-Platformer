using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCorpse : MonoBehaviour
{
    public GameObject aObject;
    public GameObject instance;
    private bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = Instantiate(aObject, transform.position, transform.rotation);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!exploded)
        {
            instance.GetComponent<CorpseExplode>().explode();
            exploded = true;
        }
        
    }
}
