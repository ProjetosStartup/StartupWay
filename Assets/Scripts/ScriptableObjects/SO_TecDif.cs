using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TecDif",menuName = "Startups/TecDif")]
public class SO_TecDif : ScriptableObject
{

    [SerializeField]
    private Util.StartupSector type;
    [SerializeField]
    private Util.Tier tier = Util.Tier.Trainee;
    [TextArea(1,10)]
    [SerializeField]
    private string description;

    private float tecDifExperience = 0;
    public Util.StartupSector Type { get => type; }
    public Util.Tier Tier { get => tier; set => tier = value; }
    public string Description { get => description; }
    
    private void ExperienceCalculator()
    {
        List<SO_Employee> especificos = StartupController.Instance.Startup.Team.Employees.FindAll(employee => employee.Function == Util.EmployeeFunction.Especifico);
        List<SO_Services> services = StartupController.Instance.Startup.Services.FindAll(services => /*services.Type == Util.Services.Tecnologia ||*/ services.Type == Util.Services.Infraestrutura);

        float bonusEspecificos = 0;
        especificos.ForEach(especifico => bonusEspecificos += (int)especifico.Tier);
       // bonusEspecificos /= 0.5f + 0.5f * especificos.Count;
     
        float bonusServices = 0;
        services.ForEach(service => bonusServices += (int)service.Tier);
        //  bonusServices /= 0.8f + 0.2f * services.Count;
     

        tecDifExperience = bonusServices + bonusEspecificos;
    }
    public void TierCalculator()
    {
        ExperienceCalculator();
        tier = Util.ExpToTier(tecDifExperience/6);
     
    }
    public void StartTecDif()
    {
        tier = Util.Tier.Trainee;
    }
}
