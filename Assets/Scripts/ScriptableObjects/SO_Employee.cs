using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Funcionario",menuName = "Funcionario/Funcionario")]
public class SO_Employee : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private new string name = Util.GetRandomName();
    [SerializeField]
    private bool isFounder;


    [SerializeField]
    private Util.EmployeeFunction function;

    [SerializeField]
    private Util.Tier tier = Util.Tier.Trainee;
    [SerializeField]
    private Util.Tier pseudoTier;
    [SerializeField]
    private float salary;

    private Sprite image;
    [SerializeField]
    private float experience;

    public bool IsFounder { get => isFounder; }

    public int Id { get => id; }

    public Util.EmployeeFunction Function { get => function; }

    public Util.Tier Tier{ get => pseudoTier; }

    public float Salary { get => salary; set => salary = Util.GetSalaryByTier(tier);  }

    public float Experience { get => experience; }

    public Sprite Image { get => image; set => image = value;  }
    public string Name { get => name; set => name = value; }

    public string Description { get => Util.EmployeeFunctionDescription(function); }

    private void OnEnable()
    {
        name = Util.GetRandomName();
        experience = (float)tier;
        pseudoTier = tier;

        salary = Util.GetSalaryByTier(tier);
    }

    public void AddExperience(float amount)
    {
        experience += amount;
        pseudoTier = Util.ExpToTier(experience);
        salary=Util.GetSalaryByTier(tier);  
    }

    public float PercentToNextTier()
    {
        float percent;
        Util.Tier tempTier;
        tempTier = Util.ExpToTier(experience);

        percent = experience - (float)tempTier;

        Debug.Log("Percent"+percent+"Experience"+experience+"tier"+(float)tempTier);


        percent /= ((float)Util.NextTier(tier) - (float)tempTier);
        Debug.Log("percent new" + percent);

        return System.Math.Abs(percent);
    }
}
