using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

[CreateAssetMenu(fileName = "Startup", menuName = "Startups/Startup")]

public class SO_Startup : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Util.StartupSector sector;
    [SerializeField]
    private string problem, solution;
    [SerializeField]
    private Util.BusinessModel businessModel = Util.BusinessModel.P2P;

    private SO_TecDif tecDif;
    [SerializeField]
    private Team team;
    private Wallet wallet;
    private List<SO_Services> services;
    private Util.Tier visibility;
    private Util.Tier productLevel;

    public List<SO_Services> Services { get => services; }
    public Team Team { get => team; }
    public SO_TecDif TecDif { get => tecDif; set => tecDif = value; }
    public Util.Tier Visibility { get => visibility; set => visibility = value; }
    public Util.Tier ProductLevel { get => productLevel; set => productLevel = value; }

    public Util.StartupSector Sector { get => sector; set => sector = value; }
    public string Problem { get => problem; set => problem = value; }
    public string Solution { get => solution; set => solution = value; }
    public Util.BusinessModel BusinessModel { get => businessModel; set => businessModel = value; }
    public string Name { get => name; set => name = value; }
    public Wallet Wallet { get => wallet; set => wallet = value; }

    public float Maturity => MaturityCalculation();
    private float prodExperience;

    private void OnEnable()
    {
        wallet = new();
        team = new();
        services = new();
        visibility = productLevel = Util.Tier.Trainee;

    }
    private float MaturityCalculation()
    {
        float exp = (float)productLevel + (float)visibility + (float)team.IntegrationTier + (float)TecDif.Tier;

        return exp * 0.25f;
    }

    private void ProductLvlXp()
    {
        List<SO_Employee> tecnics = team.Employees.FindAll(employee =>  employee.Function == Util.EmployeeFunction.Tecnico);
        List<SO_Services> tecnology = StartupController.Instance.Startup.Services.FindAll(services => services.Type == Util.Services.Tecnologia);

        float bonusTecnic = 0;
        tecnics.ForEach(tecnics => bonusTecnic += (int)tecnics.Tier);
       

        float bonusTecnology = 0;
        services.ForEach(tecnology => bonusTecnology += (int)tecnology.Tier);
       


        prodExperience = bonusTecnic + bonusTecnology;
    }
    public void TierProductLvlCalculator()
    {
        ProductLvlXp();
        productLevel = Util.ExpToTier(prodExperience / 6);

    }
}



