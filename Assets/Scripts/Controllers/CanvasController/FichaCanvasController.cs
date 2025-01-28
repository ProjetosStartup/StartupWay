using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FichaCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI startupName, sector, soluction, problem, businessModelodel ;

   


    public void AttFicha()
    {
        startupName.text = "Nome :"+StartupController.Instance.Startup.Name;
        sector.text = "Setor :"+StartupController.Instance.Startup.Sector.ToString();   
        problem.text = "Problema :\n"+StartupController.Instance.Startup.Problem;
        soluction.text= "Solução :\n"+StartupController.Instance.Startup.Solution;
        businessModelodel.text= "Modelo de Negócio :\n               "+StartupController.Instance.Startup.BusinessModel.ToString();


    }
}


