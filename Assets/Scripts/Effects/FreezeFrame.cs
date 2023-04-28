using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    private float freezeDuration;
    private float _pendingFreezeDuration;
    public bool _isFrozen = false;
    void Update()
    {
        if(_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void Freeze(float duration)
    {
        freezeDuration = duration;
        _pendingFreezeDuration = duration;
    }

    // public void setDuration(float val)
    // {
    //     duration = val;
    // }

    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(freezeDuration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0;
        _isFrozen = false;
    }
    public bool isFrozen()
    {
        return _isFrozen;
    }

}
