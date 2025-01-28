using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    // Refer�ncia � barra de progresso UI
    public Slider barraDeProgresso;
    public static Load Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Inicia uma rotina para carregar a pr�xima cena
       
    }
    private void OnEnable()
    {
        StartCoroutine(CarregarCenaAssincrona());
    }

    IEnumerator CarregarCenaAssincrona()
    {
        // Inicia o carregamento ass�ncrono da cena
        AsyncOperation operacaoCarregamento = SceneManager.LoadSceneAsync("GameScene");

        // Enquanto a cena n�o estiver completamente carregada, atualiza a barra de progresso
        while (!operacaoCarregamento.isDone)
        {
            // Calcula o progresso com base no valor de operacaoCarregamento.progress, que varia de 0 a 1
            float progresso = Mathf.Clamp01(operacaoCarregamento.progress / 0.9f); // 0.9 � o progresso m�ximo antes da ativa��o da cena

            // Atualiza a barra de progresso
            barraDeProgresso.value = progresso;

            yield return new WaitForEndOfFrame(); // Aguarda o pr�ximo quadro
        }
        this.gameObject.SetActive(false);
    }
}