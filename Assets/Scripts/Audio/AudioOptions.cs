using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptions : MonoBehaviour
{
    // Start is called before the first frame update
  public void  OpenOpitions()
    {
        GameObject audioSlider;



        audioSlider = AudioController.Instance.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Debug.Log(audioSlider.name);
        audioSlider.SetActive(true);
      
    }
}
