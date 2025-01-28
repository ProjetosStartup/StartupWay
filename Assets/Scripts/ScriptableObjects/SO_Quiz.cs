using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizQuestion", menuName = "Quiz/Question")]

public class SO_Quiz : ScriptableObject
{
    public Util.Region region;

    [TextArea(3, 5)]
    public string question;

    public Answer[] answers = new Answer[4];

    [Serializable]
    public class Answer
    {
        public float value;

        [TextArea(3,5)]
        public string text;
    }
}


