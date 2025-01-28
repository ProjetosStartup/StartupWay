using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    public void ChangeSene(string scene )
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitAplication()
    {
        Application.Quit();
    }
}
