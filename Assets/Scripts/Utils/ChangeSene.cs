using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSene : MonoBehaviour
{
    // Start is called before the first frame update
  
    public void AplicationBack(string sene)
    {
        SceneManager.LoadScene(sene);
    }
}
