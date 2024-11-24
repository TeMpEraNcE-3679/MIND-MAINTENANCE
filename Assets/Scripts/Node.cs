using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public GameManager.NODES NodeType;
    public Text Title;
    public bool IsActive;
    public string HexColorForAllDoneNode = "#C4C4C4";
    public GameObject Electrition;
    private GameManager.NODES_TUNING currentNodeTuning;
    private Color color;
    private bool hasUnlockAllConnections;
    private bool everActiveNode;
    // Start is called before the first frame update
    void Start()
    {
        currentNodeTuning = GameManager.Instance.FindNodeTuningByEnum(NodeType);
        HandleSetActive(GameManager.Instance.FindNodeTrackingByEnum(NodeType).IsUnlocked);
        ColorUtility.TryParseHtmlString(HexColorForAllDoneNode, out color);
        hasUnlockAllConnections = false;
        everActiveNode = false;
        if(Electrition!=null) Electrition.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void HandleActiveNode()
    {
        if(GameManager.Instance.FindNodeTrackingByEnum(NodeType).ConnectionData.Count != GameManager.Instance.FindNodeTrackingByEnum(NodeType).TotalConnectionNeeded)
        {
            print("You have not connect all data path to this node yet!"+ GameManager.Instance.FindNodeTrackingByEnum(NodeType).ConnectionData.Count+"/"+ GameManager.Instance.FindNodeTrackingByEnum(NodeType).TotalConnectionNeeded);
            return;
        }
        Vector3 thisPosition = this.GetComponent<RectTransform>().position;
        CameraController.Instance.HandleZoomToPosition(new Vector3(thisPosition.x, thisPosition.y, -10));
        DesciptionPageManager.Instance.HandleActivePage(currentNodeTuning);
        if (Electrition != null)
        {
            Electrition.GetComponent<Animator>().SetBool("active", true);
            Electrition.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (NodeType == GameManager.NODES.LEVEL1_KEY)
        {
            //game end;
            TipsManager.Instance.HandleShowEnding();
        }
        everActiveNode = true;
    }

    public void HandleSetActive(bool active)
    {
        IsActive = active;
        this.GetComponent<Button>().interactable = active;
        this.GetComponent<Image>().enabled = active;
        if(this.GetComponent<Animator>()!=null && NodeType != GameManager.NODES.LEVEL1_HOME_DOOR) this.GetComponent<Animator>().SetTrigger("active");
    }

    public void HandleAnimation(bool start)
    {
        this.GetComponent<Animator>().SetBool("active", start);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsActive && GameManager.Instance.FindNodeTrackingByEnum(NodeType).IsUnlocked)
        {
            HandleSetActive(true);
        }
        if (GameManager.Instance.FindNodeTrackingByEnum(NodeType).ConnectionData.Count != GameManager.Instance.FindNodeTrackingByEnum(NodeType).TotalConnectionNeeded)
        {
            this.GetComponent<Button>().interactable = false;
            if(IsActive)
            {
                if(!TipsManager.Instance.HasTriggeredOnBoarding)
                {
                    TipsManager.Instance.HandleShowOnbording();
                    TipsManager.Instance.HasTriggeredOnBoarding = true;
                    Vector3 thisPosition = this.GetComponent<RectTransform>().position;
                    CameraController.Instance.HandleZoomToPosition(new Vector3(thisPosition.x, thisPosition.y, -10));
                }
            }
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }

        if(IsActive && !hasUnlockAllConnections)
        {
            bool allUnlocked = true;
            GameManager.CONNECTION[] connections = GameManager.Instance.FindNodeTuningByEnum(NodeType).Connections;
            for (int i = 0; i < connections.Length; i++)
            {
                if(!GameManager.Instance.HasConnectionAlreadyBeMade(NodeType, connections[i].TargetNode))
                {
                    allUnlocked = false;
                    break;
                }
            }
            if(allUnlocked && everActiveNode)
            {
                hasUnlockAllConnections = true;
                this.GetComponent<Image>().color = color;
            }
        }
        

    }
}
