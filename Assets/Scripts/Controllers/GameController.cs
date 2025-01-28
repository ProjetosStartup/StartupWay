using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    
    public static GameController Instance;
    [SerializeField]
    private GameState state;

    private float maturityToWin = 126;

    public static float achivment;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Setup);
    }
    
    

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.Setup:
                ServicesCanvasController.Instance.RefreshServicesStore();
                break;

            case GameState.Quiz:
                QuizCanvasController.Instance.OpenQuiz();
                break;

            case GameState.MovePlayer:
                MovementController.Instance.SetCanMove(true);
                HireController.Instance.RefreshHireCanvas(); 
                DismissController.Instance.RefreshDismissCanvas();
                StartupController.Instance.Startup.TecDif.TierCalculator();
                StartupController.Instance.Startup.TierProductLvlCalculator();
                AtributeCanvasController.Instance.RefreshAtributeCanvas();
                AtributeCanvasController.Instance.RefreshMaturityCanvas();
                ServicesCanvasController.Instance.RefreshServicesStore();
                break;

            case GameState.RandomEvent:
                    RandomEventCanvasController.Instance.OpenEvent();
                break;

            case GameState.Payment:
                StartCoroutine(WaitPayment());
                break;

            case GameState.Verification:
                Verification();
                break;
            case GameState.Win:
                achivment = StartupController.Instance.Startup.Wallet.Balance;
                SceneManager.LoadScene("Win");
                AudioController.Instance.Play("Win");
                break;

            case GameState.Lose:
                // abrir UI de derrota
                SceneManager.LoadScene("GameOver");
                AudioController.Instance.Play("Lose");
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

    }

    public void UpdateGameState(string newState) {
        GameState newGameState = (GameState)Enum.Parse(typeof(GameState), newState);

       // UpdateGameState(newGameState);
        StartCoroutine(WhaitState(newGameState));
    }
    public GameState GetGameState() { return state; }

    private void Verification()
    {
        // verificar se deve algo (dinheiro negativo)
        if (StartupController.Instance.Startup.Wallet.Balance < 0)
        {
            // UpdateGameState(GameState.Lose);
            StartCoroutine(WhaitState(GameState.Lose));
            return;
        }

        // atualizar a ui de nivel da startup
        AtributeCanvasController.Instance.RefreshMaturityCanvas();

        // verificar se a barra de progresso chegou no maximo(ganhou)
        if (StartupController.Instance.Startup.Maturity > maturityToWin)
        {
            //UpdateGameState(GameState.Win);
            achivment = StartupController.Instance.Startup.Wallet.Balance;
            StartCoroutine(WhaitState(GameState.Win));
            return;
        }
        //atualizar atributos
        AtributeCanvasController.Instance.RefreshAtributeCanvas();
        //recomeça o loop do jogo
        StartCoroutine(WhaitState(GameState.Quiz));
       // UpdateGameState(GameState.Quiz);
    }

    private IEnumerator WhaitState(GameState state)
    {
        yield return new WaitForSeconds(1);
        UpdateGameState(state);

    }

    private IEnumerator WaitPayment()
    {
        yield return new WaitForSeconds(1);
        StartupController.Instance.Startup.Team.ExperienceDistribution();
        DismissController.Instance.RefreshDismissCanvas();
        // abrir o feedback mensal
        FeedbackCanvasController.Instance.OpenFeedback();
        // descontar o salario
        StartupController.Instance.Startup.Wallet.Balance -= StartupController.Instance.Startup.Team.TeamSalary();
        WalletCanvasController.Instance.RefreshBalanceUI(0, 1, false);
      

    }
    public enum GameState
    {
        Setup,
        MovePlayer,
        Quiz,
        RandomEvent,
        Payment,
        Verification,
        Win,
        Lose
    }

    public void QuitAplication()
    {
        Application.Quit();
    }
    public void AplicationBack()
    {
        SceneManager.LoadScene("GameScene");
    }

}

