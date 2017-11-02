using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QuestHandler : NetworkBehaviour
{
    [HideInInspector]
    public QuestObject[] quests;
    public QuestObject currentQuest;
    private Decision currentDecision;
    [SyncVar]
    int selection;
    public Text eventTitle;
    public Text eventDescription;
    public Text[] eventChoices;
    public SyncListInt votes = new SyncListInt();
    bool hasVoted = false;

    // Use this for initialization
    void Start()
    {
		
    }

    void Update()
    {
        if (isServer && votes.Count >= NetworkServer.connections.Count)
        {
            selection = votes.Pick();
            votes.Clear();
            RpcSelectDecision();
        }
        if (isServer && Input.GetKeyDown(KeyCode.S))
        {
            SelectQuest(); //Will be removed from non test version
        }
    }

    //Select a quest to run
    [Server]
    void SelectQuest()
    {
        int selectedQuestID = Random.Range(0, quests.Length);
        RpcStartQuest(selectedQuestID);
    }

    [ClientRpc]
    void RpcStartQuest(int select)
    {
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

    [ClientRpc]
    public void RpcSelectDecision()
    {
        hasVoted = false;
        int result = currentDecision.options[selection].option.onChosen();

        if (result != -1)
        {
            currentDecision = currentDecision.branches[result];
            RunQuest(currentDecision);
        }
        else {
            for (int i = 0; i < 4; ++i)
            {
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
}