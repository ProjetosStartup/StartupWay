
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;



public class GameCanvasController : MonoBehaviour
{
    public static GameCanvasController Instance;

    [SerializeField]
    private SO_EmployeeImage employeeImage;
    [SerializeField]
    private List<GameObject> infoPanels;
    [SerializeField]
    private Animator diceAnimator; 
    [SerializeField]
    private float velocity;
    [SerializeField]
    private int buttonSize;

    public string systemOperational;

    [SerializeField]
    private float regulationAspectSise;
    private float aspectRatio ;

    [SerializeField]
    private float diferenca;

    private float x, y;

    private void Awake()
    {
        Instance = this;

      
    }
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

    }

    public void ResetDiceAnimation()
    {
        diceAnimator.SetTrigger("Stopped");
    }
    public void OpenAtributosCanvas(GameObject panel)
    {
        Button button = panel.transform.GetChild(1).GetComponent<Button>();
        RectTransform panelTransform = panel.transform as RectTransform;

        panel.transform.DOMoveX(panelTransform.rect.width * aspectRatio, velocity).SetEase(Ease.InOutCubic)
            .OnStart(() => button.interactable = false)
            .OnComplete(() => button.interactable = true);
    }
    public void CloseAtributosCanvas(GameObject panel)
    {
        Button button = panel.transform.GetChild(0).GetComponent<Button>();
        Transform endPoint = panel.transform.GetChild(panel.transform.childCount - 1).transform;

        RectTransform panelTransform = panel.transform as RectTransform;

        panel.transform.DOMoveX(endPoint.position.x, velocity).SetEase(Ease.InOutCubic)
            .OnStart(() => button.interactable = false )
            .OnComplete(() => button.interactable = true);
    }
    public void OpenInfoCanvas(GameObject panel)
    {
        panel.transform.DOMoveX(Screen.width, velocity).SetEase(Ease.InOutCubic);

        List<GameObject> buttonsToHide = infoPanels.FindAll(panels => panels != panel);

       Sequence sequence = DOTween.Sequence();

        for(int i = 0; i < buttonsToHide.Count; i++)
        {
            sequence.Join(buttonsToHide[i].transform.DOMoveX(buttonsToHide[i].transform.position.x + buttonSize* aspectRatio, velocity * 0.5f));
        }

        Button button = panel.transform.GetChild(panel.transform.childCount-1).GetComponent<Button>();

        sequence.OnStart(() => button.interactable = false)
            .OnComplete(() => button.interactable = true);
    }
    public void CloseInfoCanvas(GameObject panel)
    {
        RectTransform panelTransform = panel.transform as RectTransform;
        
        panel.transform.DOMoveX(Screen.width + panelTransform.rect.width * aspectRatio, velocity).SetEase(Ease.InOutCubic);

        List<GameObject> buttonToShow = infoPanels.FindAll(panels => panels != panel);

        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < buttonToShow.Count; i++)
        {
            sequence.Join(buttonToShow[i].transform.DOMoveX(buttonToShow[i].transform.position.x - buttonSize*aspectRatio, velocity * 0.5f));
        }
        Button button = panel.transform.GetChild(panel.transform.childCount - 2).GetComponent<Button>();

        sequence.OnStart(() => button.interactable = false)
            .OnComplete(() => button.interactable = true);
    }

    public Sprite GetRandomImage()
    {
        return employeeImage.GetRandomImage();
    }
}
