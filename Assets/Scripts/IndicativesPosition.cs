using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IndicativesPosition : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform button;

    [SerializeField]
    private float regulationAspectSise;
    [SerializeField]
    private float aspectRatio;

    [SerializeField]
    private float diferenca;

    private float x, y;
    private void Update()
    {
        x = Screen.width;
        y = Screen.height;
        if ((Screen.width - 1920) == 0)
        {
            regulationAspectSise = 0;

        }
        else
        {
            regulationAspectSise = (y / x) * (x - 1920);
        }

        aspectRatio = y / (y - regulationAspectSise);
        RectTransform rectTransform = button as RectTransform;


        gameObject.transform.position = button.position;
        if (button.name == "AtributosOpenButton")
        {
            gameObject.transform.Translate(new Vector3(-5 * aspectRatio, 90 * aspectRatio, 0));
        }
        else { gameObject.transform.Translate(new Vector3(-70 * aspectRatio, -21 * aspectRatio, 0)); }

    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        RectTransform rectTransform = button as RectTransform;


        gameObject.transform.position = button.position;
        if (button.name == "AtributosOpenButton")
        {
            gameObject.transform.Translate(new Vector3(-5 * aspectRatio, 90 * aspectRatio, 0));
        }
        else { gameObject.transform.Translate(new Vector3(-70 * aspectRatio, -21 * aspectRatio, 0)); }


    }
}
