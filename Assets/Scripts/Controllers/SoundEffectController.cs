using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] clips;
    private bool playing = false;

    void Start()
    {  
        if(src == null)
        {
            src = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        }
        src = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
    }
    public void playOneShotSound(int sound)
    {
        src.PlayOneShot(clips[sound]);
    }
    public void playSound(int sound)
    {
        if(!playing)
        {
            StartCoroutine(soundCoroutine(clips[sound]));
        }
        
    }
    public void stopSound()
    {
        src.Stop();
    }
    public AudioSource getSrc()
    {
        return src;
    }

    IEnumerator soundCoroutine(AudioClip clip)
    {
        src.clip = clip;
        
        playing = true;
        src.Play();
        yield return new WaitForSeconds(clip.length);
            
        playing = false;
        
        
    }

}
