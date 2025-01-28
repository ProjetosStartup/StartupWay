using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{
   
  

    public void PlayButtonSond(string sond)
    {
        AudioController.Instance.Play(sond);    
    }
}
