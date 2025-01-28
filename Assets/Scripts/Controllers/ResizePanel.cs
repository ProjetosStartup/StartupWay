using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class ResizePanel : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        // LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());

        StartCoroutine(ResizeBox());


    }
    
    private IEnumerator ResizeBox()
    {
        this.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
    }

}
