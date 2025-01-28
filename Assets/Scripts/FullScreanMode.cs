using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FullScreanMode : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string FullScrean();
    // Start is called before the first frame update
    void Start()
    {
       // FullScrean();

    }

    public void FullScreanOn()
    {
        FullScrean();
    }

    // Update is called once per frame
   
}
