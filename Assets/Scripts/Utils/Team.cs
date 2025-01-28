using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    [SerializeField]
    private List<SO_Employee> employees = new();
    [SerializeField]
    private Util.Tier integrationTier = Util.Tier.Trainee;
    [SerializeField]
    private float teamExperience = 0;

    public Util.Tier IntegrationTier { get => integrationTier; set => integrationTier = integrationTier = value; }
    public List<SO_Employee> Employees { get => employees; }
    public void AddEmployee(SO_Employee employee) { employees.Add(employee); }
    public void RemoveEmployee(int id)
    {
        employees.Remove(employees.Find(employee => employee.Id == id));
    }

    private void ExperienceCalculator()
    {
        List<SO_Employee> lideres = employees.FindAll(employee => employee.Function == Util.EmployeeFunction.Lider);
        List<SO_Services> services = StartupController.Instance.Startup.Services.FindAll(services => services.Type == Util.Services.Treinamento);

        float bonusLider = 0;
        lideres.ForEach(lider => bonusLider += (float)lider.Tier);

        //bonusLider /= 0.5f + 0.5f * lideres.Count;

        float bonusServices = 0;
        services.ForEach(service => bonusServices += (float)service.Tier);
        //bonusServices /= 0.8f + 0.2f * services.Count;

        teamExperience = bonusServices + bonusLider;
    }


    public void TierCalculator()
    {
        integrationTier = Util.ExpToTier(teamExperience / 4);
    }

    private float ExperienceForEachEmployee()
    {
        ExperienceCalculator();
        TierCalculator();

        return (float)integrationTier / employees.Count;
    }

    public float SalaryByFunction(Util.EmployeeFunction funcao)
    {
        List<SO_Employee> nonFounderEmployees = employees.FindAll(employee => !employee.IsFounder && employee.Function == funcao);

        float teamSalary = 0;
        nonFounderEmployees.ForEach(employee => teamSalary += Util.GetSalaryByTier(employee.Tier));

        return teamSalary;
    }
    public float TeamSalary()
    {
        List<SO_Employee> nonFounderEmployees = employees.FindAll(employee => !employee.IsFounder);

        float teamSalary = 0;
        nonFounderEmployees.ForEach(employee => teamSalary += Util.GetSalaryByTier(employee.Tier));

        return teamSalary;
    }

    public void ExperienceDistribution()
    {
        float experienceToAdd = ExperienceForEachEmployee();

        employees.ForEach(employee => employee.AddExperience(experienceToAdd));
    }
}
