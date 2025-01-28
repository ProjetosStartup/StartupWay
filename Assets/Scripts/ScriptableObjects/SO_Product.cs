using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Produto", menuName = "Startups/Produto")]
public class SO_Product : ScriptableObject
{
    // tier de produto é calculado com o bonus diferencial + bonus de funcionarios tecnico
    [SerializeField]
    private Util.StartupSector type;
    [SerializeField]
    private Util.Tier tier = Util.Tier.Trainee;
    private float productExperience = 0;

    public float ProductExperience { get => productExperience; }
    private void ExperienceCalculator()
    {
        List<SO_Employee> tecnico = StartupController.Instance.Startup.Team.Employees.FindAll(employee => employee.Function == Util.EmployeeFunction.Tecnico);
        
        float bonusEspecificos = 0;
        tecnico.ForEach(tecnico => bonusEspecificos += (float)tecnico.Tier);
        bonusEspecificos /= 0.5f + 0.5f * tecnico.Count;

        float difTec = (float)StartupController.Instance.Startup.TecDif.Tier * 0.5f;

        productExperience = difTec + bonusEspecificos;
    }
    public void TierCalculator()
    {
        ExperienceCalculator();
        tier = Util.ExpToTier(productExperience);
    }

}
