using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class HireController : MonoBehaviour
{
    public static HireController Instance;

    [SerializeField]
    private GameObject hireEmployeePrefab;
    [SerializeField]
    private Transform hireContentHolder;
    [SerializeField]
    private List<SO_Employee> employeesList;

    [SerializeField]
    private GameObject hireButton, redIndicative;

    private void Awake()
    {
        Instance = this;
    }
    public void RefreshHireCanvas()
    {
        foreach (Transform child in hireContentHolder)
        {
            Destroy(child.gameObject);
        }
        float startupMaturity = StartupController.Instance.Startup.Maturity;
        int exponent = (int)Mathf.Floor(Mathf.Log(startupMaturity) / Mathf.Log(2));
        int closestPowerOfTwo = (int)Mathf.Pow(2, exponent);

        System.Random rand = new();

        var randomEmployees = employeesList.OrderBy(x => rand.Next()).Take(closestPowerOfTwo);

        foreach (var employee in randomEmployees)
        {
            SetPrefab(Instantiate(hireEmployeePrefab, hireContentHolder).transform, employee);
        }
        if(hireButton.activeSelf)
        {
            redIndicative.SetActive(true);
        }
    }
    private void SetPrefab(Transform prefab, SO_Employee employee)
    {
        string employeTierTxt, employeFunctionTxt;
        Button button = prefab.GetChild(0).GetComponent<Button>();
        Image prefabImage = prefab.GetChild(1).GetComponent<Image>();
        var function = prefab.GetChild(2).GetComponent<TextMeshProUGUI>();
        var tier = prefab.GetChild(3).GetComponent<TextMeshProUGUI>();
        var salaryText = prefab.GetChild(4).GetComponent<TextMeshProUGUI>();

        prefabImage.sprite = GameCanvasController.Instance.GetRandomImage();
        employee.Image = prefabImage.sprite;
        RandomEventCanvasController.ortografics.TryGetValue(employee.Function.ToString(),out employeFunctionTxt);
      
        function.text = $"Função: {employeFunctionTxt}";
        RandomEventCanvasController.ortografics.TryGetValue(employee.Tier.ToString(),out employeTierTxt);
        tier.text = $"Tier: {employeTierTxt}";
        string salary = string.Format(new CultureInfo("pt-BR"), "{0:N2}", employee.Salary);
        salaryText.text = $"Salário: {salary}";
        button.onClick.AddListener(() => 
        {
            StartupController.Instance.Startup.Team.AddEmployee(employee);
            prefab.gameObject.SetActive(false);
            DismissController.Instance.RefreshDismissCanvas();
            AudioController.Instance.Play("Click1");
        });
    }
}
