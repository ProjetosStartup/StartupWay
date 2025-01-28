using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServicesCanvasController : MonoBehaviour
{
    public static ServicesCanvasController Instance;

    [SerializeField]
    private GameObject buyServicePrefab,shopButton,redIndicativeService;
    [SerializeField]
    private Transform serviceStoreHolder;
    [SerializeField]
    private Transform purchasedServiceHolder;

    [SerializeField]
    private List<Sprite> iconTypes;

    private CultureInfo culture;
    private void Awake()
    {
        Instance = this;
        culture = new CultureInfo("pt-BR");
        
    }

    public void RefreshServicesStore()
    {
        foreach (Transform child in serviceStoreHolder)
            Destroy(child.gameObject);

        List<SO_Services> servicesToInstanciate = ServicesController.Instance.Services;
        
        foreach(SO_Services service in servicesToInstanciate)
        {
            SetPrefab(Instantiate(buyServicePrefab, serviceStoreHolder).transform, service);
        }

        if (shopButton.activeSelf)
        {
           redIndicativeService.SetActive(true);

        }
    }
    public void SetPrefab(Transform prefab, SO_Services service, bool isPurchased = false)
    {
        string serviceTxt;
        prefab.GetChild(0).GetComponent<Image>().sprite = iconTypes[(int)service.Type];

        RandomEventCanvasController.ortografics.TryGetValue(service.Type.ToString(), out serviceTxt);
        prefab.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{serviceTxt}";
        prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{service.Description}";
        Button buyButton = prefab.GetChild(3).GetComponent<Button>();

        if (isPurchased)
        {
            buyButton.gameObject.SetActive(false);
            return;
        }

        buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format(culture, "{0:N2}", service.Price);

        buyButton.onClick.AddListener(() =>
        {

            ServicesController.Instance.BuyService(service);
            FeedbackController.Instance.AddServices(service);
            AudioController.Instance.Play("Click1");

            if (service.Type==Util.Services.Tecnologia)
            {
                StartupController.Instance.Startup.TierProductLvlCalculator();
                AtributeCanvasController.Instance.RefreshAtributeCanvas();


            }

            else if(service.Type == Util.Services.Infraestrutura)
            {
                StartupController.Instance.Startup.TecDif.TierCalculator();
                AtributeCanvasController.Instance.RefreshAtributeCanvas();
            }


            prefab.gameObject.SetActive(false);
            RefreshServices();
        });


    }
    public void RefreshServices()
    {

        if (shopButton.activeSelf)
        {
          redIndicativeService.SetActive(true);

        }
        foreach (Transform child in purchasedServiceHolder)
        {
            Destroy(child.gameObject);
        }
         

        List<SO_Services> servicesToInstanciate = StartupController.Instance.Startup.Services;

        foreach (SO_Services service in servicesToInstanciate)
        {
            SetPrefab(Instantiate(buyServicePrefab, purchasedServiceHolder).transform, service,true);
        }
    }
}
