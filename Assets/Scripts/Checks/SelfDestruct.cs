using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifeTime = 10;
        void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
