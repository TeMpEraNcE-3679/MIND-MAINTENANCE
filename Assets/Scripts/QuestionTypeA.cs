using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTypeA : MonoBehaviour
{
    public Text Question;
    public GameObject[] Answers;
    public Text Answer;
    public int AnswerIndex;
    private GameManager.NODES_TUNING currentTuning;
    public void InitializeQuestion(GameManager.NODES_TUNING tuningData)
    {
        currentTuning = tuningData;
        Question.text = tuningData.QuestionTypeA.Question;
        for(int i = 0;i<Answers.Length;i++)
        {
            if (i < tuningData.QuestionTypeA.Answers.Length)
            {
                Answers[i].SetActive(true);
                Answers[i].GetComponent<Text>().text = tuningData.QuestionTypeA.Answers[i];
            }
            else Answers[i].SetActive(false);
        }
        Answer.text = "";
        AnswerIndex = -1;
        HandleCheckCorrect();
    }



    public void HandleCheckCorrect()
    {
        for (int i = 0; i < currentTuning.Connections.Length; i++)
        {
            if (!currentTuning.Connections[i].NeedAnswerQuestion)
            {
                GameManager.Instance.HandleNodeUnlocked(currentTuning.Connections[i].TargetNode, true,currentTuning.Type);
                continue;
            }
            if (AnswerIndex == currentTuning.Connections[i].AnswerA.AnswerIndex)
            {
                GameManager.Instance.HandleNodeUnlocked(currentTuning.Connections[i].TargetNode, true, currentTuning.Type);
                print("Connection unlock success!:" + currentTuning.Connections[i].TargetNode);
            }
        }

    }

    public void HandleOnClick(int index)
    {
        Answer.text = Answers[index].GetComponent<Text>().text;
        AnswerIndex = index;
        HandleCheckCorrect();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
