using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizCanvasController : MonoBehaviour
{
    public static QuizCanvasController Instance;
    [SerializeField]
    private GameObject quizCanvas;

    [Header("Animation")]
    [SerializeField]
    private float fadeTime = 1f;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private RectTransform questionRect;
    [SerializeField]
    private List<RectTransform> answersRect;

    [Header("Setup")]
    [SerializeField]
    private List<TextMeshProUGUI> texts;
    [SerializeField]
    private Button confirmButton;
    private int answerCorrect;

    [SerializeField]
    private GameObject alert;

    private Sequence sequence;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenQuiz()
    {
        FeedbackController.Instance.Month ++;

      
     
        SetupQuiz();
        sequence = DOTween.Sequence();

        quizCanvas.SetActive(true);
        canvasGroup.alpha = 0f;
        
        canvasGroup.DOFade(1, fadeTime);
        sequence.Append(UtilAnimation.SetScaleAndChangeScale(questionRect.transform, 0, 1, fadeTime));

        float animationToptoDown = 1; // 1 = top down, -1 = down top
        foreach (RectTransform answer in answersRect)
        {
            Vector2 pos = answer.anchoredPosition;
            answer.transform.localPosition = new Vector2(answer.transform.localPosition.x, 1000f*animationToptoDown);
            sequence.Join(answer.DOAnchorPos(pos, fadeTime).SetEase(Ease.OutSine));
            animationToptoDown *= -1;
        }
    }
    private void SetupQuiz()
    {
        SO_Quiz newQuiz = QuizController.Instance.GetRandomQuiz();

       

        texts[0].text = newQuiz.question;
        List<SO_Quiz.Answer> shuffleAnswer = newQuiz.answers.OrderBy(a => Random.value).ToList();

        for (int i = 0; i < shuffleAnswer.Count; i++)
        {
            if (shuffleAnswer[i].value==6000)
            {
                answerCorrect = i;
                break;
            }

        }

        for (int i = 0; i < 4; i++)
        {
            int index = i;
            texts[i+1].text = shuffleAnswer[i].text.ToString();
            texts[i+1].transform.parent.GetComponent<Button>().onClick.AddListener(() => ConfirmButton(shuffleAnswer[index]));
        }
    }

    private void ConfirmButton(SO_Quiz.Answer answer)
    {
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() =>
        {
            if(StartupController.Instance.Startup.Maturity >30)
            {
                StartupController.Instance.Startup.Wallet.Balance += answer.value * (StartupController.Instance.Startup.Maturity + 1) / 30;
                FeedbackController.Instance.Faturamento = answer.value * (StartupController.Instance.Startup.Maturity + 1) / 30;
            }
            else {
                StartupController.Instance.Startup.Wallet.Balance += answer.value;
                FeedbackController.Instance.Faturamento = answer.value ;
            }
           
            

            if(MovementController.Instance.TotalJumps>42)
            {
                if(StartupController.Instance.Startup.Team.Employees.Count < 2) 
                {
                    
                    StartCoroutine(WaitAlert());
                    StartupController.Instance.Startup.Wallet.Balance -= MovementController.Instance.TotalJumps * 50;
                    FeedbackController.Instance.Faturamento -= MovementController.Instance.TotalJumps * 50; 

                }
            }
            

            if (answer.value <= 3000) { AudioController.Instance.Play("Erro"); }
            else { AudioController.Instance.Play("Correto"); }
            WalletCanvasController.Instance.RefreshBalanceUI(1,1);
        });
    }

    private IEnumerator WaitAlert()
    {
        yield return new WaitForSeconds(2.5f);
        alert.SetActive(true);
    }
    public void CloseQuiz()
    {
        WalletController.Instance.AddBalance(new BalanceWallet());
        FeedbackController.Instance.ResetFeedback();

        StartCoroutine(TimeCloseQuis());
    }

    public  IEnumerator TimeCloseQuis()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                answersRect[i].GetChild(j).gameObject.GetComponent<Image>().color = new Color(1, 0.6169811f, 0.6169811f,1);

            }

        }
        switch (answerCorrect)
        {
            case 0:
                answersRect[0].GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.7206601f,1, 0.4660377f, 1);
                break;
            case 1:
                answersRect[0].GetChild(1).gameObject.GetComponent<Image>().color = new Color(0.7206601f, 1, 0.4660377f, 1);
                break;
            case 2:
                answersRect[1].GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.7206601f, 1, 0.4660377f, 1);
                break;
            case 3:
                answersRect[1].GetChild(1).gameObject.GetComponent<Image>().color = new Color(0.7206601f, 1, 0.4660377f, 1);
                break;

        }
        yield return new WaitForSeconds(2);
       
        canvasGroup.DOFade(0, fadeTime).OnComplete(() => quizCanvas.SetActive(false));
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                answersRect[i].GetChild(j).gameObject.GetComponent<Image>().color = Color.white;

            }

        }
    }
}
