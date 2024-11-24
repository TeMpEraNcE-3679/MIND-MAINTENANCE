using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum NODES
    {
        LEVEL1_HOME_DOOR,
        LEVEL1_ELEVATOR,
        LEVEL1_MAIL_BOX,
        LEVEL1_MAIL_BOX_KEY,
        LEVEL1_KEY_STRING_AND_CARD_HOLDER,
        LEVEL1_BENCH,
        LEVEL1_KEY,
        LEVEL1_APARTMENT_ENTRANCE,
        LEVEL1_SUPERMARKET,
        LEVEL1_SHOPPING,
        LEVEL1_TOY,
        LEVEL1_CARD_HOLDER,
        LEVEL1_DROP_OFF_BUS_STOP,
        LEVEL1_SEAT,
        LEVEL1_CARD_READER,
        LEVEL1_CONVERSATION,
        LEVEL1_WEEKEND_SCHEDULE,
        LEVEL1_DRINK,
        LEVEL1_VENDING_MACHINE,
        LEVEL1_VENDING_MACHINE_CARD_SLOT,
        LEVEL1_BOARDING_BUS_STOP,
        LEVEL1_BARBECUE_RESTAURANT,
        LEVEL1_SCHOOL_GATE,
        LEVEL1_HALLWAY,
        LEVEL1_CLASSROOM_PACKING,
        LEVEL1_FEELING_THIRSTY,
    }
    [Serializable]
    public struct CONNECTION
    {
        public NODES TargetNode;
        public bool NeedAnswerQuestion;
        public QUESTION_ANSWER_A AnswerA;
        public QUESTION_ANSWER_B AnswerB;
    }

    public enum QUESTION_TYPE
    {
        TYPE_A,
        TYPE_B,
    }

    [Serializable]
    public struct QUESTION_TYPE_A
    {
        public string Question;
        public string[] Answers;
    }
    [Serializable]
    public struct QUESTION_ANSWER_A
    {
        public int AnswerIndex;
    }



    [Serializable]
    public struct QUESTION_TYPE_B
    {
        public string Question;
        public string[] AnswerPartA;
        public string[] AnswerPartB;
    }

    [Serializable]
    public struct QUESTION_ANSWER_B
    {
        public int[] AnswerIndexs;
    }


    [Serializable]
    public struct NODES_TUNING
    {
        public NODES Type;
        public string Description;
        public NODES[] NodesPointedToThis;
        public CONNECTION[] Connections;
        public QUESTION_TYPE QuestionType;
        public QUESTION_TYPE_A QuestionTypeA;
        public QUESTION_TYPE_B QuestionTypeB;
        public string Title;
        public Sprite BackGround;
    }

    public struct NODE_TRACKING_DATA_PER_CONNECTION
    {
        public NODE_TRACKING_DATA_PER_CONNECTION(NODES From) { FromNode = From;IsUnlocked = true; }
        public NODES FromNode;
        public bool IsUnlocked;
    }


    public struct NODE_TRACKING_DATA
    {
        public NODES Type;
        public bool IsUnlocked;
        public List<NODE_TRACKING_DATA_PER_CONNECTION> ConnectionData;
        public int TotalConnectionNeeded;
    }


    public NODES_TUNING[] NodesTuning;
    private NODE_TRACKING_DATA[] NodesTracking;

    public NODES_TUNING FindNodeTuningByEnum(NODES node)
    {
        for(int i = 0;i<NodesTuning.Length;i++)
        {
            if (NodesTuning[i].Type == node)
            {
                return NodesTuning[i];
            }
        }
        Debug.LogError("Could not find such node:" + node.ToString());
        return NodesTuning[0];
    }

    public bool HasConnectionAlreadyBeMade(NODES from, NODES to)
    {
        for(int i = 0;i< FindNodeTrackingByEnum(to).ConnectionData.Count;i++)
        {
            if(FindNodeTrackingByEnum(to).ConnectionData[i].FromNode == from)
            {
                return true;
            }
        }
        return false;
    }

    public void HandleNodeUnlocked(NODES node, bool unlocked,NODES from)
    {
        for (int i = 0; i < NodesTracking.Length; i++)
        {
            if (NodesTracking[i].Type == node)
            {
                NodesTracking[i].IsUnlocked = unlocked;
                if(!HasConnectionAlreadyBeMade(from, node))
                {
                    NodesTracking[i].ConnectionData.Add(new NODE_TRACKING_DATA_PER_CONNECTION(from));
                }
            }
        }
    }


    public NODE_TRACKING_DATA FindNodeTrackingByEnum(NODES node)
    {
        for (int i = 0; i < NodesTracking.Length; i++)
        {
            if (NodesTracking[i].Type == node)
            {
                return NodesTracking[i];
            }
        }
        Debug.LogError("Could not find such node:" + node.ToString());
        return NodesTracking[0];
    }

    public int GetTotalConnectionForNode(NODES node)
    {
        int count = 0;
        for (int i = 0; i < NodesTuning.Length; i++)
        {
           for(int j = 0;j<NodesTuning[i].Connections.Length;j++)
            {
                if(NodesTuning[i].Connections[j].TargetNode == node)
                {
                    count++;
                }
            }
        }
        return count;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        NodesTracking = new NODE_TRACKING_DATA[NodesTuning.Length];
        for (int i = 0; i < NodesTracking.Length; i++)
        {
            NodesTracking[i].Type = NodesTuning[i].Type;
            NodesTracking[i].IsUnlocked = NodesTracking[i].Type == NODES.LEVEL1_HOME_DOOR;
            NodesTracking[i].ConnectionData = new List<NODE_TRACKING_DATA_PER_CONNECTION>();
            NodesTracking[i].TotalConnectionNeeded = GetTotalConnectionForNode(NodesTracking[i].Type);
        }
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
