using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Util;
//using UnityEngine.UIElements;

public class AtributeCanvasController : MonoBehaviour
{
    public static AtributeCanvasController Instance;

    [SerializeField] private Slider productSlider;
    [SerializeField] private Slider difSlider;
    [SerializeField] private Slider teamSlider;
    [SerializeField] private Slider visibilitySlider;
    [SerializeField] private Slider maturitySlider;
    [SerializeField] private GameObject redIndicative;

    public bool combination;

    [SerializeField]
    private GameObject buttonIsOn;

    public GameObject ButtonIsOn { get => buttonIsOn; set => buttonIsOn = value; }
    public GameObject RedIndicative { get => redIndicative; set => redIndicative = value; }


    private void Awake()
    {
        Instance = this;
       
    }
   
    public void RefreshAtributeCanvas()
    {
       
       
        productSlider.value = (float)StartupController.Instance.Startup.ProductLevel;
        difSlider.value = (float)StartupController.Instance.Startup.TecDif.Tier;
        teamSlider.value = (float)StartupController.Instance.Startup.Team.IntegrationTier;
        visibilitySlider.value = (float)StartupController.Instance.Startup.Visibility;
       
       

        print("produto:" + productSlider.value);
        print("diferencial:" + difSlider.value);
        print("time:" + teamSlider.value);
        print("visibilidade" + visibilitySlider.value);
    }
    public void RefreshMaturityCanvas()
    {
        maturitySlider.value = (int)StartupController.Instance.Startup.Maturity;
    }

    public void RedIndicativeTributes()
    {
        if (buttonIsOn.activeSelf)
        {
            redIndicative.SetActive(true);
        }
    }

    public void Verification()
    {
        if (AtributeCanvasController.Instance.VerifCompability())
        {

           
            StartupController.Instance.Startup.Visibility = Util.Tier.Pleno;

        }
        else
        {
           
            StartupController.Instance.Startup.Visibility = Util.Tier.Trainee;
        }

        productSlider.value = (float)StartupController.Instance.Startup.ProductLevel;
        difSlider.value = (float)StartupController.Instance.Startup.TecDif.Tier;
        teamSlider.value = (float)StartupController.Instance.Startup.Team.IntegrationTier;
        visibilitySlider.value = (float)StartupController.Instance.Startup.Visibility;
    }

    public  bool VerifCompability()
    {
        switch(StartupController.Instance.Startup.Sector)
        {
            case Util.StartupSector.Healthtech:
                if(StartupController.Instance.Startup.TecDif.Type==Util.StartupSector.Fintech)
                {return true;}

                else if(StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                {return true;}

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Legaltech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Martech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Govtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else {return false; }

            case Util.StartupSector.Cleantech:
                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Legaltech:
                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Edtech)
                { return true; }

               
                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }



                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Legaltech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Martech:
                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }



                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Agrotech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Traveltech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Edtech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Edtech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Agrotech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Legaltech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Fintech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Edtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Legaltech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Martech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else { return false; }

            case Util.StartupSector.Govtech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Edtech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Fintech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }


                else { return false; }

            case Util.StartupSector.Logtech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }


                else { return false; }

            case Util.StartupSector.Traveltech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Agrotech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }


                else { return false; }

            case Util.StartupSector.Proptech:

                if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Proptech)
                { return true; }


                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Agrotech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Cleantech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Legaltech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Logtech)
                { return true; }

                else if (StartupController.Instance.Startup.TecDif.Type == Util.StartupSector.Healthtech)
                { return true; }


                else { return false; }
        }
        return false;
    }
}
