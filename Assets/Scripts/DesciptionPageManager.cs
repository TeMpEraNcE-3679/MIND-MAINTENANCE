using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesciptionPageManager : MonoBehaviour
{
    public static DesciptionPageManager Instance;
    public GameObject DescriptionPage;
    public Text Title;
    public Text Description;
    public GameObject QuestionTypeA;
    public GameObject QuestionTypeB;
    public Image BackGroundImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ResetPage()
    {
        DescriptionPage.SetActive(false);
    }

    public void HandleActivePage(GameManager.NODES_TUNING tuningData)
    {
        DescriptionPage.SetActive(true);
        Title.text = tuningData.Title;
        Description.text = tuningData.Description;
        BackGroundImage.sprite = tuningData.BackGround;
        if (tuningData.QuestionType == GameManager.QUESTION_TYPE.TYPE_A)
        {
            QuestionTypeA.SetActive(true);
            QuestionTypeB.SetActive(false);
            QuestionTypeA.GetComponent<QuestionTypeA>().InitializeQuestion(tuningData);
        }
        else
        {
            QuestionTypeA.SetActive(false);
            QuestionTypeB.SetActive(true);
            QuestionTypeB.GetComponent<QuestionTypeB>().InitializeQuestion(tuningData);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DescriptionPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
