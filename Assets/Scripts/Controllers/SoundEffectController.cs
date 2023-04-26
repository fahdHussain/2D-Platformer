using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] clips;

    void Start()
    {
        src = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }
    public void playSound(int sound)
    {
        src.PlayOneShot(clips[sound]);
    }
    public void stopSound()
    {
        src.Stop();
    }
    public AudioSource getSrc()
    {
        return src;
    }

}
