using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseExplode : MonoBehaviour
{
    public GameObject[] objects;
    public float downAngle = -30;
    public float speed = 5f;

    private float segmentSize;
    private float[] segments;
    private Vector2[] vectors;
    

    void Start()
    {
        
        
    }
    private void calcAngles()
    //range: downAngle -> 180 -downAngle
    {
        float curStartAngle = downAngle;

        for(int i = 0; i < objects.Length; i++)
        {
            float rand = Random.Range(curStartAngle, curStartAngle + segmentSize);
            segments[i] = rand;
            curStartAngle += segmentSize;
        }
    }
    private void calcVectors()
    // x = cos(angle in Rads)
    //y = sin(angle in Rads)
    {
        for(int i = 0; i < vectors.Length; i ++)
        {
            float x = Mathf.Cos(segments[i] * Mathf.Deg2Rad);
            float y = Mathf.Sin(segments[i] * Mathf.Deg2Rad);
            
            vectors[i] = new Vector2(x,y);
        }
    }
    public void explode()
    {
        segments = new float[objects.Length];
        segmentSize = (180 + (-2*downAngle))/objects.Length;
        vectors = new Vector2[objects.Length];
        calcAngles();
        calcVectors();
        for(int i = 0; i < vectors.Length; i++)
        {
            objects[i].GetComponent<Rigidbody2D>().AddForce(vectors[i]*speed, ForceMode2D.Impulse);
        }
    }



}
