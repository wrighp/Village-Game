using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QuestHandler : NetworkBehaviour
{
    public static QuestHandler i;
    [HideInInspector]
    public QuestObject[] quests;
    public QuestObject currentQuest;
    private Decision currentDecision;

    public Text eventTitle;
    public Text eventDescription;
    public Text[] eventChoices;
    public SyncListInt votes = new SyncListInt();
    [SyncVar]
    public int busy = 0;
    bool hasVoted = false;
    public bool isVoting = false;
    public GameObject EventExit;

    // Use this for initialization
    void Start()
    {
        i = this;
    }

    void Update()
    {
        if (isServer && votes.Count >= NetworkServer.connections.Count)
        {
            int selection = votes.Pick();
            votes.Clear();
            RpcSelectDecision(selection);
        }
    }

    //Select a quest to run
    [Server]
    public void SelectQuest()
    {
        busy = NetworkServer.connections.Count;
        int selectedQuestID = Random.Range(0, quests.Length);
        votes.Clear();
        RpcStartQuest(selectedQuestID);
    }

    [ClientRpc]
    void RpcStartQuest(int select)
    {
        isVoting = true;
        transform.GetChild(0).gameObject.SetActive(true);
        currentQuest = quests[select];
        currentDecision = currentQuest.Decision;
        eventTitle.text = currentQuest.QuestName;
        RunQuest(quests[select].Decision);
    }

    public void RunDecision(int select)
    {
        if (!hasVoted)
        {
            print("Appear");
			Cmds.i.CmdQuestVote(select);
            hasVoted = true;
        }
    }

    void RunQuest(Decision decision)
    {
        eventDescription.text = decision.decisionDescription;
        for (int i = 0; i < 4; ++i)
        {
            if (decision.options.Length > i &&
              decision.options[i].option.IsVisible())
            {
                eventChoices[i].enabled = true;
                eventChoices[i].text = decision.options[i].choiceDescription;
                if (currentDecision.options[i].option.isSelectable())
                {
                    eventChoices[i].color = new Color(1, 1, 1);
                    eventChoices[i].GetComponent<Button>().enabled = true;
                }
                else {
                    eventChoices[i].color = new Color(100f / 255, 0, 0);
                    eventChoices[i].GetComponent<Button>().enabled = false;
                }
            }
            else {
                print("Disabled");
                eventChoices[i].enabled = false;
            }
        }
    }

    public void ExitQuest()
    {
        Cmds.i.CmdExit();
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        isVoting = false;
    }

    [ClientRpc]
    public void RpcSelectDecision(int selection)
    {
        print("Runnning " + selection);
        hasVoted = false;
        int result = currentDecision.options[selection].option.onChosen();
        print("Result " + result); 
        if (result != -1)
        {
            currentDecision = currentDecision.branches[result];
            RunQuest(currentDecision);
        }
        else {
            for (int i = 0; i < 4; ++i)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                eventChoices[i].enabled = false;
            }
        }
    }
}

public partial class Cmds
{
    [Command]
    public void CmdQuestVote(int selection)
    {
        GameObject.FindObjectOfType<QuestHandler>().votes.Add(selection);
    }

    [Command]
    public void CmdExit()
    {
        GameObject.FindObjectOfType<QuestHandler>().busy--;
    }
}