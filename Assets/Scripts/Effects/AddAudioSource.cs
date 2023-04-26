using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAudioSource : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(audioSource, transform.position, transform.rotation);
    }
}
