using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Newtonsoft.Json;

public static class Util
{

    private static readonly string [] namesContainer = {
    "Ana","Anderson", "André", "Amanda", "Arthur", "Beatriz", "Bernardo", "Bianca","Bigenius", "Bruno", "Breu", "Camila", "Carlos",
    "Carolina", "Cauã", "Clara", "Cristiano", "Cristina", "Daniel", "Débora", "Diego", "Eduarda", "Eduardo",
    "Emily", "Enzo", "Evelyn", "Felipe", "Fernanda", "Fernando", "Gabriela", "Gabriel", "Genilson", "Giovana", "Gustavo",
    "Helena", "Henrique", "Isabela", "Isaac", "Isadora", "Joaquim","Joelma", "Juliana", "Juliano", "Julia", "Kauan",
    "Laís", "Leonardo", "Leandro", "Letícia", "Lucas", "Luisa", "Luiz", "Mariana", "Mário", "Mariane", "Mateus",
    "Melissa", "Miguel", "Natalia", "Nicolas", "Nicole", "Otávio", "Pâmela", "Paulo", "Priscila", "Rafael",
    "Raquel", "Renan", "Roberta", "Rodrigo", "Rogério", "Samuel", "Sarah", "Silvana", "Thiago", "Valentina",
    "Vitor", "Vitória", "Vinícius", "Yasmin", "Yago", "Zilda", "Zé", "Zara", "Zacarias", "Zuleide",
    "Wagner", "Wanessa", "William", "Wesley", "Xuxa", "Xavier", "Ximena", "Xande", "Xenia", "Xisto",
    "Ubirajara", "Ursula", "Ulisses", "Uéliton", "Umberto", "Vanderlei", "Valéria", "Vicente", "Violeta", "Verônica",
    "Wladimir", "Wanda", "Washington", "Weslley", "Wilma", "Xena", "Xango", "Yuri",
    "Yasmim", "Yago", "Yane", "Yanna", "Zaqueu", "Zara", "Zeca", "Zélia", "Zico", "Zoe",
    "Zózimo", "Zélia", "Zayra", "Zenilda", "Zoroastra", "Zahia", "Zack", "Zilá", "Zóia", "Zuleica"
};
    
    public enum Tier
    {
        Zero = 0,
        Trainee = 2,
        Junior = 4,
        Pleno = 8,
        Senior = 16,
        Master = 32,
        Mestre = 64,
        Doutor = 128
    }

    public enum SalaryByTier
    {
        Zero = 0,
        Trainee = 500,
        Junior = 1000,
        Pleno = 1500,
        Senior = 2000,
        Master = 2500,
        Mestre = 3000,
        Doutor = 3500
    }
    public static float GetSalaryByTier(Tier tier)
    {
        switch (tier)
        {
            case Tier.Zero:
                return (float)SalaryByTier.Zero;    
               
            case Tier.Trainee:
                return(float)SalaryByTier.Trainee;
               
            case Tier.Junior:
                return (float)SalaryByTier.Junior;
              
            case Tier.Pleno:
                return (float)SalaryByTier.Pleno;
               
            case Tier.Senior:
                return (float)SalaryByTier.Senior;
                
            case Tier.Master:
                return (float)SalaryByTier.Master;  
                
            case Tier.Mestre:
                return (float)SalaryByTier.Mestre;
               
            case Tier.Doutor:
                return (float)SalaryByTier.Doutor;
              
            default:
                return (float)SalaryByTier.Trainee;
               
        }
       
    }

   
    public static List<Tier> TierList = new List<Tier> { Tier.Doutor, Tier.Mestre, Tier.Master, Tier.Senior, Tier.Pleno, Tier.Junior, Tier.Trainee };
   

    public static Tier PreviousTier(Tier actualTier)
    {
        int actualIndex = TierList.IndexOf(actualTier);

        if (actualIndex == -1 || actualIndex == TierList.Count - 1)
            return Tier.Zero;

        else
            return TierList.ElementAtOrDefault(actualIndex + 1);
    }
    public static Tier NextTier(Tier actualTier)
    {
        int actualIndex = TierList.IndexOf(actualTier);

        if (actualIndex == -1 || actualIndex == 0)
            return TierList.FirstOrDefault();
        else
            return TierList[actualIndex - 1];
    }
   
  
    public enum EmployeeFunction
    {
        Especifico,
        Vendedor,
        Tecnico,
        Lider
    }
    public static string EmployeeFunctionDescription(EmployeeFunction function)
    {
        return function switch
        {
            EmployeeFunction.Especifico => "Responsável por tarefas específicas dentro da empresa.",
            EmployeeFunction.Vendedor => "Encarregado de vendas e atendimento ao cliente.",
            EmployeeFunction.Tecnico => "Realiza atividades técnicas e de manutenção.",
            EmployeeFunction.Lider => "Supervisiona e coordena equipes de trabalho.",
            _ => $"TODO: {function}",
        };
    }
    public enum StartupSector
    {
        Healthtech,
        Fintech,
        Edtech,
        Proptech,
        Agrotech,
        Cleantech,
        Legaltech,
        Logtech,
        Martech,
        Traveltech,
        Govtech
    }

    public enum StartupNames
    {
        MedFusion,
        PayWave,
        LearnSphere,
        HomeWise,
        AgriBoost,
        GreenPulse,
        LawStream,
        ShipSync,
        MarketGenius,
        WanderWise,
        CivicLink
    }

    public enum Services
    {
        Treinamento = 0,
        Tecnologia,
        Infraestrutura
    }

    public enum BusinessModel
    {
        P2P,
        D2C
    }

    public static Tier ExpToTier(float exp)
    {
        foreach (Tier tier in TierList)
        {   
            if (exp >= (float)tier)
                return tier;
        }            
       // Se exp for menor que todos os limites retorna (Comum)
        return Tier.Trainee;
    }
    

    public static string GetRandomName() {
        System.Random randomGenerator = new();
        int randomIndex = randomGenerator.Next(0, namesContainer.Length);
        return namesContainer[randomIndex]; 
    }

    public enum Region
    {
        CG,
        Dourados,
        All
    }
}
