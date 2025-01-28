using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomEvent", menuName = "Quiz/RandomEvent")]
public class SO_RandomEvent : ScriptableObject
{
    [Header("Região")]
    public Util.Region region;

    [Header("Valores após decisão")]
    public float successValue;
    public float failureValue;
    

    [Header("Preço do Evento")]
    public float price,rentValue;

    [Header("Condição para Sucesso")]
    public ConditionToSuccess[] conditions;
    [Space(10)]
    [Header("Texto do Evento")]
    [TextArea(3, 5)]
    public string text;


    [Serializable]
    public class ConditionToSuccess
    {
        public float quantity = 1;
        public Util.EmployeeFunction employeeFunction = Util.EmployeeFunction.Especifico;
        public Util.Tier employeeTier = Util.Tier.Trainee;
    }

}
