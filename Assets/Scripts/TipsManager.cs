using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    public static TipsManager Instance;
    public Sprite[] IntroSprites;
    public Sprite[] TutorialSprites;
    public Sprite[] OnBordingSprites;
    public Sprite[] OutroSprites;
    public Image ImageComponent;
    public GameObject FadeBackground;
    public GameObject TipsUI;
    public bool HasTriggeredOnBoarding;
    public GameObject FirstNode;
    private int index;
    private bool introEnd;
    private bool tutorialEnd;
    private bool onbordingEnd;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        ImageComponent.sprite = IntroSprites[index];
        FadeBackground.SetActive(true);
        introEnd = false;
        onbordingEnd = false;
        tutorialEnd = false;
        HasTriggeredOnBoarding = false;
    }

    public void HandleShowNextIntroSprite()
    {
        index++;
        if (!introEnd)
        {
            if(index < IntroSprites.Length) ImageComponent.sprite = IntroSprites[index];
            else
            {
                FadeBackground.SetActive(false);
                index = 0;
                introEnd = true;
                ImageComponent.sprite = TutorialSprites[index];
                FirstNode.GetComponent<Node>().HandleAnimation(true);
            }
        }
        else if(!tutorialEnd)
        {
            if (index < TutorialSprites.Length)
            {
                FirstNode.GetComponent<Node>().HandleAnimation(false);
                ImageComponent.sprite = TutorialSprites[index];
            }
            else
            {
                tutorialEnd = true;
                TipsUI.SetActive(false);
            }
        }
        else if(!onbordingEnd)
        {
            //onbording
            if (index < OnBordingSprites.Length) ImageComponent.sprite = OnBordingSprites[index];
            else
            {
                onbordingEnd = true;
                TipsUI.SetActive(false);
            }
        }
        else
        {
            if (index < OutroSprites.Length) ImageComponent.sprite = OutroSprites[index];
        }
    }

    public void HandleShowOnbording()
    {
        if (onbordingEnd) return;
        TipsUI.SetActive(true);
        index = 0;
        ImageComponent.sprite = OnBordingSprites[index];
    }

    public void HandleShowEnding()
    {
        TipsUI.SetActive(true);
        index = 0;
        ImageComponent.sprite = OutroSprites[index];
        FadeBackground.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
