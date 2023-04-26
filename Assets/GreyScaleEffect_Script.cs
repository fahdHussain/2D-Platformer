using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class GreyScaleEffect_Script : MonoBehaviour
{
    PostProcessVolume grey_volume;
    ColorGrading colorGrading;
    public void SpawnGreyScale()
    {
        colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        colorGrading.enabled.Override(true);
        colorGrading.saturation.Override(-100f);

        grey_volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, colorGrading);
        StartCoroutine(timer());
    }
    public void reset()
    {
        colorGrading.saturation.Override(0f);
    }

    void OnDestroy()
    {
        if(grey_volume != null)
        {
            RuntimeUtilities.DestroyVolume(grey_volume, true, true);
        }
         }

    IEnumerator timer()
    {
 
        yield return new WaitForSecondsRealtime(3);
        reset();
    }
}
