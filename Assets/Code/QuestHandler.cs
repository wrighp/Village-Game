using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour {
    public QuestObject[] quests;

    public QuestObject currentQuest;
    private Decision currentDecision;

    public Text eventTitle;
    public Text eventDescription;
    public Text[] eventChoices;

    // Use this for initialization
	void Start () {
        SelectQuest();
    }

    //Select a quest to run
    void SelectQuest(){
        currentQuest = quests.Pick();
        currentDecision = currentQuest.Decision;
        eventTitle.text = currentQuest.QuestName;
        RunQuest(currentDecision);
    }

    void RunQuest(Decision decision){
        eventDescription.text = decision.decisionDescription;
        for(int i = 0; i < 4; ++i){
            if(decision.options.Length > i &&
              decision.options[i].option.IsVisible()){
                print("Enabled");
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

    public void RunDecision(int selection){

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
