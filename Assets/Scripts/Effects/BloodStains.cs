using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStains : MonoBehaviour
{
    public GameObject[] bloodStains;
    Vector3 scaleChange;
    //public float x_ScaleMin = 1;
    //public float x_ScaleMax = 3;

    //public float y_ScaleMin = 1;
    //public float y_ScaleMax = 3;

    public float scaleMin = 1;
    public float scaleMax = 1;
    public float alphaRange_min = 0.8f;
    public float alphaRange_max = 0.95f;
    private Vector3 drop;


    public void SpawnBlood(Transform location, int type)
    {
        //Type: 0 == regular damage
        //Type: 1 == Spike
        int rand = Random.Range(0, bloodStains.Length);
        GameObject instance = Instantiate(bloodStains[rand], location.position, location.rotation);
        
        float x_Scale = Random.Range(scaleMin, scaleMax);
        float y_Scale = Random.Range(scaleMin, scaleMax);

        scaleChange = new Vector3(x_Scale, y_Scale, 0);
        if(type == 0)
        {
            //Regular
            drop  = new Vector3(0, 0.5f, 0);  
        }
        if(type == 1)
        {
            //Spike
            drop  = new Vector3(0, 0.8f, 0);  
        }

        instance.transform.localScale += scaleChange;
        instance.transform.position -= drop;
        

        float alpha = Random.Range(alphaRange_min, alphaRange_max);
        float r = instance.GetComponent<SpriteRenderer>().color.r;
        float b = instance.GetComponent<SpriteRenderer>().color.b;
        float g = instance.GetComponent<SpriteRenderer>().color.g;
        
        Color transparent = new Vector4(r,g,b,alpha);

        instance.GetComponent<SpriteRenderer>().color = transparent;
    }
    
}
