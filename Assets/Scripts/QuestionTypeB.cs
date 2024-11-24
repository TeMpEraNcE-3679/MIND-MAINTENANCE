using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTypeB : MonoBehaviour
{
    public Text Question;
    public GameObject[] AnswersA;
    public Text AnsweredQuestionA;
    public int AnswerAIndex;
    public GameObject[] AnswersB;
    public Text AnsweredQuestionB;
    public int AnswerBIndex;
    private GameManager.NODES_TUNING currentTuning;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void InitializeQuestion(GameManager.NODES_TUNING tuningData)
    {
        currentTuning = tuningData;
        Question.text = tuningData.QuestionTypeB.Question;

        for (int i = 0; i < AnswersA.Length; i++)
        {
            if (i < tuningData.QuestionTypeB.AnswerPartA.Length)
            {
                AnswersA[i].SetActive(true);
                AnswersA[i].GetComponent<Text>().text = tuningData.QuestionTypeB.AnswerPartA[i];
              //  AnswersA[i].GetComponent<Button>().onClick.AddListener(() => { HandleOnClickA(i); });
            }
            else AnswersA[i].SetActive(false);
        }
        for (int i = 0; i < AnswersB.Length; i++)
        {
            if (i < tuningData.QuestionTypeB.AnswerPartB.Length)
            {
                AnswersB[i].SetActive(true);
                AnswersB[i].GetComponent<Text>().text = tuningData.QuestionTypeB.AnswerPartB[i];
               // AnswersB[i].GetComponent<Button>().onClick.AddListener(() => { HandleOnClickB(i); });
            }
            else AnswersB[i].SetActive(false);
        }
        AnsweredQuestionA.text = "";
        AnsweredQuestionB.text = "";
        AnswerAIndex = -1;
        AnswerBIndex = -1;
        HandleCheckCorrect();
    }

    public void HandleCheckCorrect()
    {
        for(int i = 0;i<currentTuning.Connections.Length;i++)
        {
            if(!currentTuning.Connections[i].NeedAnswerQuestion)
            {
                GameManager.Instance.HandleNodeUnlocked(currentTuning.Connections[i].TargetNode, true, currentTuning.Type);
                continue;
            }
            if (AnswerAIndex == currentTuning.Connections[i].AnswerB.AnswerIndexs[0]
                && AnswerBIndex == currentTuning.Connections[i].AnswerB.AnswerIndexs[1])
            {
                GameManager.Instance.HandleNodeUnlocked(currentTuning.Connections[i].TargetNode,true, currentTuning.Type);
                print("Connection unlock success!:" + currentTuning.Connections[i].TargetNode);
            }
        }
        
    }


    public void HandleOnClickA(int index)
    {
        AnsweredQuestionA.text = AnswersA[index].GetComponent<Text>().text;
        AnswerAIndex = index;
        HandleCheckCorrect();
    }

    public void HandleOnClickB(int index)
    {
        AnsweredQuestionB.text = AnswersB[index].GetComponent<Text>().text;
        AnswerBIndex = index;
        HandleCheckCorrect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
