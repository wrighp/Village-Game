using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QuestHandler : NetworkBehaviour{
    public QuestObject[] quests;

    public QuestObject currentQuest;
    private Decision currentDecision;
    [SyncVar]int selection;
    public Text eventTitle;
    public Text eventDescription;
    public Text[] eventChoices;
    public List<int> votes;

    // Use this for initialization
	void Start () {
        if (hasAuthority) {
            votes = new List<int>();
            //SelectQuest(); //Will be removed from non test version
        }
    }

    void FixedUpdate() {
        if (hasAuthority && votes.Count >= Network.connections.Length){
            selection = votes.Pick();
            votes.Clear();
            CmdSelectDecision();
        }
    }

    //Select a quest to run
    void SelectQuest() {
        if (hasAuthority) {
            int selectedQuestID = Random.Range(0, quests.Length);
            CmdStartQuest(selectedQuestID);
        }
    }

    [Command(channel = 0)]
    void CmdStartQuest(int selection){
        currentQuest = quests[selection];
        currentDecision = currentQuest.Decision;
        eventTitle.text = currentQuest.QuestName;
        RunQuest(quests[selection].Decision);
    }

    void RunQuest(Decision decision){
        eventDescription.text = decision.decisionDescription;
        for(int i = 0; i < 4; ++i){
            if(decision.options.Length > i &&
              decision.options[i].option.IsVisible()){
                eventChoices[i].enabled = true;
                eventChoices[i].text = decision.options[i].choiceDescription;
                if (currentDecision.options[i].option.isSelectable()){
                    eventChoices[i].color = new Color(1,1,1);
                    eventChoices[i].GetComponent<Button>().enabled = true;
                } else {
                    eventChoices[i].color = new Color(100f/255, 0, 0);
                    eventChoices[i].GetComponent<Button>().enabled = false;
                }
            } else {
                print("Disaled");
                eventChoices[i].enabled = false;
            }
        }
    }

    [Command(channel = 0)]
    public void CmdRunDecision(int selection) {
        if(hasAuthority)
            votes.Add(selection);
    }

    [Command(channel = 0)]
    public void CmdSelectDecision() {
        int result = currentDecision.options[selection].option.onChosen();

        if(result != -1) {
            currentDecision = currentDecision.branches[result];
            RunQuest(currentDecision);
        } else {
            for (int i = 0; i < 4; ++i) {
                eventChoices[i].enabled = false;
            }
        }
    }
}
