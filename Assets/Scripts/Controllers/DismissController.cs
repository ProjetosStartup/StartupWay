using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DismissController : MonoBehaviour
{
    public static DismissController Instance;

    [SerializeField]
    private GameObject dismissEmployeePrefab;
    [SerializeField]
    private Transform dismissContentHolder;

    private void Awake()
    {
        Instance = this;
    }
    public void RefreshDismissCanvas()
    {
        foreach (Transform child in dismissContentHolder)
            Destroy(child.gameObject);

        foreach (var employee in StartupController.Instance.Startup.Team.Employees)
            SetPrefab(Instantiate(dismissEmployeePrefab, dismissContentHolder).transform, employee);

    }
    private void SetPrefab(Transform prefab, SO_Employee employee)
    {
        string employeTierTxt, employeFunctionTxt;
        Button button = prefab.GetChild(0).GetComponent<Button>();
        Image prefabImage = prefab.GetChild(1).GetComponent<Image>();
        var function = prefab.GetChild(2).GetComponent<TextMeshProUGUI>();
        var tier = prefab.GetChild(3).GetComponent<TextMeshProUGUI>();
        var salaryText = prefab.GetChild(4).GetComponent<TextMeshProUGUI>();
        RandomEventCanvasController.ortografics.TryGetValue(employee.Function.ToString(), out employeFunctionTxt);
       
        prefabImage.sprite = employee.Image;
        function.text = $"Função: {employeFunctionTxt}";
        RandomEventCanvasController.ortografics.TryGetValue(employee.Tier.ToString(), out employeTierTxt);
        tier.text = $"Tier: {employeTierTxt}";
        string salary = string.Format(new CultureInfo("pt-BR"), "{0:N2}",Util.GetSalaryByTier(employee.Tier));
        salaryText.text = $"Salário: {salary}";

        if(employee.IsFounder)
        {
            button.gameObject.SetActive(false);
            salaryText.text = $"<b>Fundador</b>";
        }
        else
        {
            button.onClick.AddListener(() =>
            {
                StartupController.Instance.Startup.Team.RemoveEmployee(employee.Id);
                prefab.gameObject.SetActive(false);
                AudioController.Instance.Play("Click1");
            });
        }
            var expImage = prefab.GetChild(5).GetComponent<Image>();


        expImage.fillAmount = employee.PercentToNextTier();

    }
}
