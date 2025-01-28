using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FeedbackController : MonoBehaviour
{
    public static FeedbackController Instance;
    private int month = 0;
    private float inicialBalance = 0, faturamento = 0;
    private float despesaEventoAleatorio;
    private List<SO_Services> despesaServicos = new();
    public int Month { get => month; set => month = value;}
    public float DespesaEventoAleatorio { get => despesaEventoAleatorio; set => despesaEventoAleatorio = value; }
    public float DespesaEspecifico => StartupController.Instance.Startup.Team.SalaryByFunction(Util.EmployeeFunction.Especifico);
    public float DespesaVendedor => StartupController.Instance.Startup.Team.SalaryByFunction(Util.EmployeeFunction.Vendedor);
    public float DespesaTecnico => StartupController.Instance.Startup.Team.SalaryByFunction(Util.EmployeeFunction.Tecnico);
    public float DespesaLider => StartupController.Instance.Startup.Team.SalaryByFunction(Util.EmployeeFunction.Lider);
    public List<SO_Services> DespesasServicos => despesaServicos;
    public float Faturamento { get => faturamento; set => faturamento = value; }
    public float InicialBalance => inicialBalance;
    public float EndBalance => BalanceCalculation();

    private float BalanceCalculation()
    {
        float despesaSer = despesaServicos.Sum(s => s.Price);
        float salario = StartupController.Instance.Startup.Team.TeamSalary();

        return inicialBalance + faturamento - despesaEventoAleatorio - despesaSer - salario;
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void ResetFeedback()
    {
        inicialBalance = StartupController.Instance.Startup.Wallet.Balance;
        faturamento = 0;
        despesaEventoAleatorio = 0;
        despesaServicos = new();
    }

    public void AddServices(SO_Services service)
    {
        despesaServicos.Add(service);
    }
    
}
