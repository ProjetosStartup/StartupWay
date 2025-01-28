using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ServicesController : MonoBehaviour
{
    public static ServicesController Instance;
    
    [SerializeField]
    private List<SO_Services> services;

    public List<SO_Services> Services => services.OrderBy(q => Random.value).ToList();

    private void Awake()
    {
        Instance = this;
    }

    public void BuyService(SO_Services service)
    {
        StartupController.Instance.Startup.Wallet.Balance -= service.Price;
        WalletCanvasController.Instance.RefreshBalanceUI(0, 1, false);
       
       
        StartupController.Instance.Startup.Services.Add(service);
        StartupController.Instance.Startup.Wallet.Balance += 0;
        StartCoroutine(WaitCoin());
    }
     private IEnumerator WaitCoin()
    {
        yield return new WaitForEndOfFrame();
        StartupController.Instance.Startup.Wallet.Balance += 0;
    }


}
