using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStains : MonoBehaviour
{
    public GameObject[] bloodStains;
    Vector3 scaleChange;
    public float x_ScaleMin = 1;
    public float x_ScaleMax = 3;

    public float y_ScaleMin = 1;
    public float y_ScaleMax = 3;

    public float y_drop = -10;
    public float alphaRange_min = 0.8f;
    public float alphaRange_max = 0.95f;


    public void SpawnBlood(Transform location)
    {
        int rand = Random.Range(0, bloodStains.Length);
        GameObject instance = Instantiate(bloodStains[rand], location.position, location.rotation);
        
        float x_Scale = Random.Range(x_ScaleMin, x_ScaleMax);
        float y_Scale = Random.Range(y_ScaleMin, y_ScaleMax);

        scaleChange = new Vector3(x_Scale, y_Scale, 0);
        Vector3 drop  = new Vector3(0, y_drop, 0); 

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
