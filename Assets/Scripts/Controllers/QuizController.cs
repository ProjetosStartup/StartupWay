using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizController : MonoBehaviour
{
    public static QuizController Instance;
 
    [SerializeField]
    private List<SO_Quiz> quizzes;

    public List<SO_Quiz> Quizzes => quizzes;
    public List<SO_Quiz> quizzesCoppy;
    private int totalCount;
    private bool inout;

    private void Awake()
    {
        Instance = this;
        totalCount = quizzes.Count;
        inout = true;
    }
    public SO_Quiz GetRandomQuiz()
    {
        Util.Region region = MovementController.Instance.ActualTile.region;
        SO_Quiz temp = new SO_Quiz();

        if (inout)
        {
            temp = quizzes.Where(q => q.region == region || q.region == Util.Region.All).OrderBy(q => Random.value).FirstOrDefault();
            quizzesCoppy.Add(temp);
            quizzes.Remove(temp);
        }
        else
        {
            temp = quizzesCoppy.Where(q => q.region == region || q.region == Util.Region.All).OrderBy(q => Random.value).FirstOrDefault();
            quizzes.Add(temp);
            quizzesCoppy.Remove(temp);
        }

        if (quizzes.Count == 0 )
        {
          inout = false;

        }
        else if  (quizzes.Count == totalCount)
        {
           inout = true;
        }

        return temp;
    }
}
