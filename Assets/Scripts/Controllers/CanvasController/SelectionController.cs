using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
//using static UnityEditor.MaterialProperty;
using Unity.VisualScripting;

public class SelectionController : MonoBehaviour
{
    #region Startup
    [Header("Startup")]
    [SerializeField]
    private GameObject startupSelectionContent;
    [SerializeField]
    private List<SO_Startup> startupList;
    [SerializeField]
    private GameObject startupIconPrefab;
    [SerializeField]
    private Transform selectedStartupIcon;
 
    private SO_Startup selectedStartup;

    [Header("StartupCreation")]
    [SerializeField]
    private TMP_InputField creationName,problem,soluction;
    [SerializeField]
    private TMP_Dropdown creationSector,creationBusinessModel;

    #endregion

    #region Founder
    [Header("Founder")]
    [SerializeField]
    private GameObject founderSelectionContent;
    [SerializeField]
    private Transform founderPanel;
    [SerializeField]
    private List<SO_Employee> founderList;
    [SerializeField]
    private GameObject founderIconPrefab;
    [SerializeField]
    private List<Transform> selectedFoundersIcons;
    
    private List<SO_Employee> selectedFounders = new();
    #endregion

    [Header("TecDif")]
    [SerializeField]
    private GameObject tecDifSelectionContent;
    [SerializeField]
    private List<SO_TecDif> tecDifList;
    [SerializeField]
    private GameObject tecDifIconPrefab;
    [SerializeField]
    private Transform selectedTecDifIcon,tecdifPanel;
    private SO_TecDif selectedTecDif;

    [Header("Button")]
    [SerializeField]
    private Button confirmButton;
    [Header("Animation")]
    [SerializeField]
    private float duration = 1f;
    private void Start()
    {
        foreach(SO_Startup startup in startupList)
        {
            SetStartupPrefab(startup);
        }
        System.Random rand = new();

        var randomEmployees = founderList.OrderBy(x => rand.Next());
        foreach (SO_Employee founder in randomEmployees)
        {
            SetFounderPrefab(founder);
        }
        foreach(SO_TecDif tecDif in tecDifList)
        {
            tecDif.Tier = Util.Tier.Trainee;
            SetTecDifPrefab(tecDif);
        }
    }
    #region TecDifRegion
    private void SetTecDifPrefab(SO_TecDif tecDif)
    {
        string typeTxt;
        GameObject instantiateTecDif = Instantiate(tecDifIconPrefab, tecDifSelectionContent.transform);

        Transform tecDifInfo = instantiateTecDif.transform.GetChild(0);

        TextMeshProUGUI tecDifType = tecDifInfo.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tecDifDescription = tecDifInfo.GetChild(1).GetComponent<TextMeshProUGUI>();

        RandomEventCanvasController.ortografics.TryGetValue(tecDif.Type.ToString(), out typeTxt);
        tecDifType.text = typeTxt;
        tecDifDescription.text = $"<color=blue><b>Descrição</b></color>\n{tecDif.Description}";
        Button sectionButton = tecDifInfo.transform.GetChild(2).GetComponent<Button>();

        sectionButton.onClick.AddListener(() => TecDifSelectionButton(tecDif));
    }
    private void TecDifSelectionButton(SO_TecDif tecdif)
    {
        string typeTxt;
        AudioController.Instance.Play("Click1");
        
        //UtilAnimation.ChangeScaleAndDesactive(GameObject.FindGameObjectWithTag("StartupSelectionCanvas").transform,0, 0.2f, DG.Tweening.Ease.InBounce);
        GameObject.FindGameObjectWithTag("TecDifSelectionCanvas").SetActive(false);
        selectedTecDif = tecdif;
        selectedTecDifIcon.gameObject.SetActive(true);
        RandomEventCanvasController.ortografics.TryGetValue(tecdif.Type.ToString(), out typeTxt);
        selectedTecDifIcon.GetChild(0).GetComponent<TextMeshProUGUI>().text = typeTxt;
        selectedTecDifIcon.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<color=blue><b>Descrição</b></color>\n\n{tecdif.Description}";

        UtilAnimation.SetScaleAndChangeScale(founderPanel, 0, 1, duration, DG.Tweening.Ease.OutBack);
        founderPanel.gameObject.SetActive(true);
       

    }

    #endregion

    #region StartupRegion
    private void SetStartupPrefab(SO_Startup startup)
    {
        GameObject instantiateStartup = Instantiate(startupIconPrefab, startupSelectionContent.transform);

        Transform startupInfo = instantiateStartup.transform.GetChild(0);

        TextMeshProUGUI startupName = startupInfo.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI startupDescription = startupInfo.GetChild(1).GetComponent<TextMeshProUGUI>();

        startupName.text = startup.Name;

        startupDescription.text = "<color=blue><b>Setor</b></color>\n" + startup.Sector.ToString() +
            "\n\n<color=blue><b>Problema</b></color>\n" + startup.Problem +
            "\n\n<color=blue><b>Solução</b></color>\n" + startup.Solution +
            "\n\n<color=blue><b>Modelo de negócio</b></color>\n" + startup.BusinessModel;

        Button sectionButton = startupInfo.transform.GetChild(2).GetComponent<Button>();
        sectionButton.onClick.AddListener(() => StartupSelectionButton(startup));
    }

    private void StartupSelectionButton(SO_Startup startup)
    {
        //UtilAnimation.ChangeScaleAndDesactive(GameObject.FindGameObjectWithTag("StartupSelectionCanvas").transform,0, 0.2f, DG.Tweening.Ease.InBounce);
        GameObject.FindGameObjectWithTag("StartupSelectionCanvas").SetActive(false);
        selectedStartup = startup;
        selectedStartupIcon.gameObject.SetActive(true);
        selectedStartupIcon.GetChild(0).GetComponent<TextMeshProUGUI>().text = startup.Name;
        selectedStartupIcon.GetChild(1).GetComponent<TextMeshProUGUI>().text = "<color=blue><b>Setor</b></color>\n" + startup.Sector.ToString() +
            "\n\n<color=blue><b>Modelo de negócio</b></color>\n" + startup.BusinessModel;
        UtilAnimation.SetScaleAndChangeScale(tecdifPanel, 0, 1, duration, DG.Tweening.Ease.OutBack);
        tecdifPanel.gameObject.SetActive(true);
        AudioController.Instance.Play("Click1");

    }
    public void CreateStartupSO()
    {
        SO_Startup newStartup = ScriptableObject.CreateInstance<SO_Startup>();
        
        if(creationName.text == "")
            creationName.text = "Startup";

        newStartup.Name = creationName.text;
        newStartup.Sector = (Util.StartupSector)creationSector.value;
        newStartup.BusinessModel = (Util.BusinessModel)creationBusinessModel.value;
        newStartup.Problem = problem.text;
        newStartup.Solution = soluction.text;

        StartupSelectionButton(newStartup);
    }
    #endregion

    #region FounderRegion
    private void SetFounderPrefab(SO_Employee founder)
    {
        string founderFunctionTxt;
        GameObject instantiateFounder = Instantiate(founderIconPrefab, founderSelectionContent.transform);

        Transform founderInfo = instantiateFounder.transform.GetChild(0);

        TextMeshProUGUI founderName = founderInfo.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI founderFunction = founderInfo.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI founderDescription = founderInfo.GetChild(2).GetComponent<TextMeshProUGUI>();
        Image founderImage = founderInfo.GetChild(3).GetComponent<Image>();
        RandomEventCanvasController.ortografics.TryGetValue(founder.Function.ToString(), out founderFunctionTxt);
        founderName.text = founder.Name;
        founderFunction.text = $"<color=blue><b>Função</b></color>\n{founderFunctionTxt}";
        founderDescription.text = $"<color=blue><b>Descrição</b></color>\n{founder.Description}";
        founder.Image = GameCanvasController.Instance.GetRandomImage();
        founderImage.sprite = founder.Image;
        
        Button sectionButton = founderInfo.transform.GetChild(4).GetComponent<Button>();
        sectionButton.onClick.AddListener(() => FounderSelectionButton(founder));

        
    }
    private void FounderSelectionButton(SO_Employee founder)
    {
        //UtilAnimation.ChangeScaleAndDesactive(GameObject.FindGameObjectWithTag("StartupSelectionCanvas").transform,0, 0.2f, DG.Tweening.Ease.InBounce);
        GameObject.FindGameObjectWithTag("FounderSelectionCanvas").SetActive(false);
        AudioController.Instance.Play("Click1");
        selectedFounders.Add(founder);
        selectedFoundersIcons[selectedFounders.Count - 1].gameObject.SetActive(true);
        selectedFoundersIcons[selectedFounders.Count - 1].GetChild(0).GetComponent<TextMeshProUGUI>().text = founder.Name;
        selectedFoundersIcons[selectedFounders.Count - 1].GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<color=blue><b>Função</b></color>\n{founder.Function}";
        selectedFoundersIcons[selectedFounders.Count - 1].GetChild(2).GetComponent<Image>().sprite = founder.Image;
        
        if (selectedFounders.Count == 2)
            confirmButton.gameObject.SetActive(true);
    }

    #endregion
    public void ConfirmButton()
    {
       //tartupController.Instance.Startup.TecDif.StartTecDif();
        StartupController.Instance.Startup = selectedStartup;
        StartupController.Instance.Startup.TecDif = selectedTecDif;
        AudioController.Instance.Play("Click1");
        AtributeCanvasController.Instance.Verification();

        foreach (SO_Employee founder in selectedFounders)
            StartupController.Instance.Startup.Team.AddEmployee(founder);

        GameController.Instance.UpdateGameState(GameController.GameState.Quiz);
    }
}
