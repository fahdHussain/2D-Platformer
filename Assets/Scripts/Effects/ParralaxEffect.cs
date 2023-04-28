using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    private float _startingPos;
    private float _lengthOfSprite;
    public float AmountOfParallax;
    public Camera MainCamera;
    void Start()
    {   
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();;
        _startingPos = transform.position.x;
        _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = MainCamera.transform.position;
        float temp = Position.x*(1 - AmountOfParallax);
        float distance = Position.x * AmountOfParallax;

        Vector3 NewPosition = new Vector3(_startingPos + distance, transform.position.y, transform.position.z);
        transform.position = NewPosition;
    }
}
