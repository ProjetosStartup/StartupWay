using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FeedbackCanvasController : MonoBehaviour
{
    public static FeedbackCanvasController Instance;

    [SerializeField]
    private GameObject feedbackPanel;

    [SerializeField]
    private TextMeshProUGUI month,inicialBalanceAndFaturamento,endBalance;

    [SerializeField]
    private TextMeshProUGUI especifico, vendedor, tecnico, lider;

    [SerializeField]
    private TextMeshProUGUI treinamento, tecnologia, infra;

    [SerializeField]
    private TextMeshProUGUI eventos;
    [SerializeField]
    private TextMeshProUGUI saldoFinal;


  
    private float balanceServices;



    private void Awake()
    {
        Instance = this;
    }
    public void CloseFeedback()
    {
       // FeedbackController.Instance.ResetFeedback();
       
        
        feedbackPanel.SetActive(false);
       
       
    }
    public void OpenFeedback()
    {
        feedbackPanel.SetActive(true);
        WalletController.Instance.RefreshButton();
       
       
        month.text = $"Feedback do {FeedbackController.Instance.Month}º mês";
      
        inicialBalanceAndFaturamento.text = $"<b><color=#BFD02B>Saldo Inicial:</color></b> {FeedbackController.Instance.InicialBalance}\n" +
          $"<b><color=#BFD02B>Faturamento({FeedbackController.Instance.Month}° mês):</color></b> {FeedbackController.Instance.Faturamento}";

      
      

        SetCustos();
        SetServices();
        SetOutros();
        saldoFinal.text = $"<b><color=#BFD02B>Saldo Final:</color></b> {FeedbackController.Instance.EndBalance}";

              

     
       
        
    }
    public  BalanceWallet BalanceRefresh()
    {

       
        BalanceWallet balance = new BalanceWallet();
        balance.Capital = FeedbackController.Instance.InicialBalance;
        balance.Invoicing = FeedbackController.Instance.Faturamento;

        balance.Employees = FeedbackController.Instance.DespesaEspecifico + 
                            FeedbackController.Instance.DespesaLider +
                            FeedbackController.Instance.DespesaTecnico +
                            FeedbackController.Instance.DespesaVendedor;


        balance.Services = FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Treinamento).Sum(s => s.Price)+
                            FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Tecnologia).Sum(s => s.Price)+
                            FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Infraestrutura).Sum(s => s.Price);
        balance.Others = FeedbackController.Instance.DespesaEventoAleatorio;
        balance.EndBalance = FeedbackController.Instance.EndBalance;
     
        return balance;

    }

    private void SetOutros()
    {
        eventos.gameObject.SetActive(FeedbackController.Instance.DespesaEventoAleatorio > 0);
        eventos.text = $"Eventos: {FeedbackController.Instance.DespesaEventoAleatorio}";
    }

    private void SetServices()
    {

        float treinamento = FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Treinamento).Sum(s => s.Price);
        float tecnologia = FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Tecnologia).Sum(s => s.Price);
        float infra = FeedbackController.Instance.DespesasServicos.Where(s => s.Type == Util.Services.Infraestrutura).Sum(s => s.Price);
        balanceServices=treinamento+tecnologia+infra;   

        this.treinamento.gameObject.SetActive(treinamento > 0);
        this.tecnologia.gameObject.SetActive(tecnologia > 0);
        this.infra.gameObject.SetActive(infra > 0);

        this.treinamento.text = $"• Treinamento: {treinamento}";
        this.tecnologia.text = $"• Técnologia: {tecnologia}";
        this.infra.text = $"• Infraestrutura: {infra}";
        feedbackPanel.SetActive(false);
        feedbackPanel.SetActive(true);
        
        
    }

    private void SetCustos()
    {
        especifico.gameObject.SetActive(FeedbackController.Instance.DespesaEspecifico > 0);
        lider.gameObject.SetActive(FeedbackController.Instance.DespesaLider > 0);
        tecnico.gameObject.SetActive(FeedbackController.Instance.DespesaTecnico > 0);
        vendedor.gameObject.SetActive(FeedbackController.Instance.DespesaVendedor > 0);

        especifico.text = $"• Específico: {FeedbackController.Instance.DespesaEspecifico}";
        lider.text = $"• Líder: {FeedbackController.Instance.DespesaLider}";
        tecnico.text = $"• Técnico: {FeedbackController.Instance.DespesaTecnico}";
        vendedor.text = $"• Vendedor: {FeedbackController.Instance.DespesaVendedor}";

       
       
    }
}
