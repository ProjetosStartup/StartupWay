using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScale : MonoBehaviour
{
    [SerializeField]
    private Transform[] butons;
   

    // Start is called before the first frame update
   public void ResetLayersButons()
    {

        for (int i = 0; i < butons.Length; i++) 
        {
           
            UtilAnimation.ChangeScale(butons[i], 1, 1);
        }
    }

    public void ChageScaleButon(int selectButon)
    {
        ResetLayersButons();

        
        UtilAnimation.ChangeScale(butons[selectButon], 1.1f, 1);
        AudioController.Instance.Play("Click2");
    }
}
