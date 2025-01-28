using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;

public class WalletController : MonoBehaviour
{
    public static WalletController Instance;
  
    public  List<BalanceWallet> walletList  = new List<BalanceWallet>();

    [SerializeField]
    TextMeshProUGUI txtValues;

    [SerializeField]
    TMP_Dropdown dropdown;

    //Use these for adding options to the Dropdown List
    private TMP_Dropdown.OptionData m_NewData;
    //The list of messages for the Dropdown
    private List<TMP_Dropdown.OptionData> m_Messages = new List<TMP_Dropdown.OptionData>();


    // Start is called before the first frame update


    void Awake()
    {
        
        Instance = this;    
       
    }
  
    public void AddBalance(BalanceWallet balance)
    {

        walletList.Add(balance);


      
        m_NewData = new TMP_Dropdown.OptionData();
        m_NewData.text = $"Mes {FeedbackController.Instance.Month}º";
        dropdown.options.Add(m_NewData);
       
        dropdown.value=walletList.Count;
      //  Refresh(FeedbackCanvasController.Instance.BalanceRefresh());


    }

    public void RefreshButton()
    {
        walletList[walletList.Count - 1] = FeedbackCanvasController.Instance.BalanceRefresh();
        dropdown.value = walletList.Count;
        Refresh();
        


    }

    public  void Refresh()
    {

        BalanceWallet balance = new BalanceWallet();
        Debug.Log("mudolavor");
        foreach (var wallet in walletList) { Debug.Log(wallet.Invoicing); }



        if (walletList.Count > 1)
        {

            balance.Invoicing = walletList.Where(s => walletList.IndexOf(s) <= dropdown.value).Sum(s => s.Invoicing);
            balance.Services = walletList.Where(s => walletList.IndexOf(s) <= dropdown.value).Sum(s => s.Services);
            balance.Employees = walletList.Where(s => walletList.IndexOf(s) <= dropdown.value).Sum(s => s.Employees);
            balance.Others = walletList.Where(s => walletList.IndexOf(s) <= dropdown.value).Sum(s => s.Others);


    }
        else
        {
            balance.Invoicing = walletList[walletList.Count - 1].Invoicing;
            balance.Services = walletList[walletList.Count - 1].Services;
            balance.Employees = walletList[walletList.Count - 1].Employees;
            balance.Others = walletList[walletList.Count - 1].Others;
        }


if (walletList.Count>0 ) 
        {
            txtValues.text = $"{balance.Invoicing.ToString("C2")}\n\n" +
                             $"{balance.Employees.ToString("C2")}\n" +
                             $"{balance.Services.ToString("C2")}\n" +
                             $"{balance.Others.ToString("C2")}\n";
                            
         
   
        }
    }



    // Update is called once per frame
  
}
