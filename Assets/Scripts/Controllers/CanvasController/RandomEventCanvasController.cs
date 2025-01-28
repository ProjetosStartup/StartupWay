using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventCanvasController : MonoBehaviour
{
    public static RandomEventCanvasController Instance;

    private Sequence sequence;

    [SerializeField]
    private GameObject eventCanvas;

    [SerializeField]
    private GameObject[] okFunction;

    [SerializeField]
    private GameObject [] functionsGameObject;
    [Header("Setup")]
    [SerializeField]
    private TextMeshProUGUI text, gain, rentCost, cost;


    [Header("Setup")]
    [SerializeField]
    private TextMeshProUGUI [] functon, tier;

    [SerializeField]
    private Toggle rent;

    public Toggle Rent { get => rent; set => rent = value; }

   

    [SerializeField] private Image gainImage;

  

   [SerializeField]
    private Button confirmButton;

    [Header("Animation")]
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private RectTransform questionRect;
    [SerializeField]
    private RectTransform buttons;

    private float randonEventXp;
    private Util.Tier tierRandonEvent;
   


    private float rentValuecopy,gainvalueCopy,costCopy;

   public static Dictionary<string, string> ortografics = new Dictionary<string, string>
    {

        { "Vendedor", "Vendedor" },

        { "Lider", "Líder" },

        { "Especifico", "Específico" },

        { "Tecnico", "Técnico" },

        { "Trainee", "Trainee" },

        { "Junior", "Júnior" },

        { "Pleno", "Pleno" },

        { "Senior", "Sênior" },

        { "Master", "Master" },

        { "Mestre", "Mestre" },

        { "Doutor", "Doutor" },

        { "Treinamento", "Treinamento" },

        { "Tecnologia", "Técnologia" },

        { "Infraestrutura", "Infraestrutura" }


    };


    private void Awake()
    {
        Instance = this;
        randonEventXp = 0;
    }
    public void OpenEvent()
    {
        rent.isOn = false;   
        SetupEvent();
        sequence = DOTween.Sequence();

        eventCanvas.SetActive(true);

        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, fadeTime);

        sequence.Append(UtilAnimation.SetScaleAndChangeScale(questionRect.transform, 0, 1, fadeTime));

        Vector2 pos = buttons.anchoredPosition;
        buttons.transform.localPosition = new Vector2(buttons.transform.localPosition.x, -500f);
        sequence.Join(buttons.DOAnchorPos(pos, fadeTime).SetEase(Ease.OutSine));
    }

   

    private void SetupEvent()
    {
        SO_RandomEvent newEvent = RandomEventController.Instance.GetRandomEvent();
        rentValuecopy = newEvent.rentValue;
        gainvalueCopy = newEvent.successValue;
        costCopy = newEvent.price;

        text.text = newEvent.text;
      

        rentCost.text = newEvent.rentValue.ToString("C2");

        if (VerifyCondition(newEvent.conditions))
        {
            gain.text=newEvent.successValue.ToString("C2");
            cost.text = newEvent.price.ToString("C2");
          
          //  gainImage.color=Color.green;
           
            rent.interactable = false;
            confirmButton.interactable = true;
            confirmButton.gameObject.GetComponent<Image>().color = Color.white;

        }
        else
        {
            rent.interactable = true;
           
           // gainImage.color = Color.red;
           
           
            cost.text = (newEvent.price).ToString("C2");
            gain.text = newEvent.successValue.ToString("C2");
            confirmButton.interactable = false;
            confirmButton.gameObject.GetComponent<Image>().color = new Color(1,1,1,0.5f);


        }

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => ConfirmButton(newEvent));

    }
    private void ConfirmButton(SO_RandomEvent newEvent)
    {
        // Wallet wallet = StartupController.Instance.Startup.Wallet;
        //if(!rent.isOn)
        //{
        //    StartupController.Instance.Startup.Visibility = Util.ExpToTier((float)StartupController.Instance.Startup.Visibility);//Util.NextTier(StartupController.Instance.Startup.Visibility);
        //    AtributeCanvasController.Instance.RefreshAtributeCanvas();
           
          

        //}
        
      
       
        if (VerifyCondition(newEvent.conditions))
        {
           
            StartupController.Instance.Startup.Wallet.Balance += (newEvent.successValue - newEvent.price) ;
            FeedbackController.Instance.Faturamento += newEvent.successValue;

           
            FeedbackController.Instance.DespesaEventoAleatorio += newEvent.price;
            if(newEvent.failureValue<=1000)
                randonEventXp += 8;
            else if (newEvent.failureValue <= 2000)
                randonEventXp += 16;
            else if (newEvent.failureValue <= 3000)
                randonEventXp += 32;


            tierRandonEvent = StartupController.Instance.Startup.Visibility;
            StartupController.Instance.Startup.Visibility = Util.ExpToTier(randonEventXp);

            if((int)tierRandonEvent<(int)StartupController.Instance.Startup.Visibility)
            {
                randonEventXp = (float)StartupController.Instance.Startup.Visibility;
            }

            WalletController.Instance.walletList[WalletController.Instance.walletList.Count - 1].Others += newEvent.price;
       
            WalletCanvasController.Instance.RefreshBalanceUI(1, 1, true);
          
        }
        else
        {
            confirmButton.gameObject.GetComponent<Image>().color = Color.white;
            if (rent.isOn)
            {
                StartupController.Instance.Startup.Wallet.Balance += (newEvent.successValue - newEvent.price-newEvent.rentValue);
                FeedbackController.Instance.Faturamento += newEvent.successValue;
                FeedbackController.Instance.DespesaEventoAleatorio += newEvent.price+newEvent.rentValue;
                WalletController.Instance.walletList[WalletController.Instance.walletList.Count - 1].Invoicing += (newEvent.successValue );
                WalletController.Instance.walletList[WalletController.Instance.walletList.Count - 1].Others += newEvent.price + newEvent.rentValue;
                WalletCanvasController.Instance.RefreshBalanceUI(1, 1, true);

                StartupController.Instance.Startup.Wallet.Balance += 0;
            }
            
           
        }
        WalletController.Instance.RefreshButton();
        AtributeCanvasController.Instance.RefreshAtributeCanvas();
       

        CloseRandomEvent();
    }

    public void RentChanges()
    {
        if (!rent.isOn)
        {
          
            
            gain.text =(gainvalueCopy).ToString("C2");
            cost.text = (costCopy).ToString("C2");
           // gainImage.color = Color.red;
            confirmButton.interactable = false;
            confirmButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            
            gain.text = (gainvalueCopy).ToString("C2");
            cost.text = (costCopy+rentValuecopy).ToString("C2");
            
           //
           //gainImage.color = Color.green;
            confirmButton.interactable = true;
            confirmButton.gameObject.GetComponent<Image>().color =Color.white;



        }
    }

    private bool VerifyCondition(SO_RandomEvent.ConditionToSuccess[] conditions)
    {
        var team = StartupController.Instance.Startup.Team.Employees;
        bool[] allConditions=new bool[conditions.Length];
        bool finalConditions = true;
        string funcTxt, tierTxt;

        for (int k = 0; k < conditions.Length; k++)
        {
            functionsGameObject[k].SetActive(true);
            ortografics.TryGetValue((conditions[k].employeeFunction.ToString()),out  funcTxt);
            ortografics.TryGetValue((conditions[k].employeeTier.ToString()), out tierTxt);

            functon[k].text = funcTxt;
            tier[k].text = tierTxt;   

        }

        for (int j = 0; j < conditions.Length; j++)
        {
           // int employeesAccepted = 0;
            bool collorFunctionOk=false;
            
            
         
            // Conta apenas os funcionários que atendem ao tier e à função da condição
            foreach (var employee in team)
            {
                if ((int)employee.Tier >= (int)conditions[j].employeeTier && employee.Function == conditions[j].employeeFunction)
                {
                   

                   // employeesAccepted++;
                    collorFunctionOk = true;

                    // Se já atingiu a quantidade necessária, sai do loop interno
                    //if (employeesAccepted >= conditions[j].quantity)
                    //{
                       
                    //    //break;
                    //}
                       
                }
              
            }
          
            if(collorFunctionOk)
            {
                functionsGameObject[j].GetComponent<Image>().color = Color.white;
                okFunction[j].SetActive(true);
                allConditions[j] = true;
            }
            else
            {
                functionsGameObject[j].GetComponent<Image>().color = Color.black;
                allConditions[j] = false;

            }
            


            // Se o número de funcionários aceitos for menor que a quantidade especificada na condição, retorna false
           
        }
        foreach(bool condition in allConditions) 
        {
            if(!condition)
            {
                finalConditions = false;
            }
        }

        if (finalConditions)
        {
            return true;
        }
        else
        {
            return false;
        }
        // Se todas as condições forem atendidas para todas as condições, retorna true
       
    }

    public void CloseRandomEvent()
    {
        foreach (GameObject funcions in functionsGameObject)
        {
            funcions.GetComponent<Image>().color = Color.black;
            funcions.SetActive(false);
        }
        foreach (GameObject isOK in okFunction)
        {
            isOK.SetActive(false);
        }
        canvasGroup.DOFade(0, fadeTime).OnComplete(() =>
        {
            eventCanvas.SetActive(false);
            GameController.Instance.UpdateGameState(GameController.GameState.Payment);
            WalletCanvasController.Instance.RefreshBalanceUI(1, 1,true);
        });
    }
}
