using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Achivments : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Material gold, silver, coper;

    [SerializeField]
    private MeshRenderer capivara;

    [SerializeField]
    private TextMeshProUGUI text;
    void Start()
    {
        if (GameController.achivment >= 200000)
        {
            text.text = "1º Lugar Capivara de Ouro";
            capivara.material = gold;
        }
        else if (GameController.achivment >= 100000 && GameController.achivment < 200000)
        {
            text.text = "2º Lugar Capivara de Prata";
            capivara.material = silver;


        }
        else
        {
            text.text = "3º Lugar Capivara de Cobre";
            capivara.material = coper;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
