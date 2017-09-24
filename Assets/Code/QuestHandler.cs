using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class QuestHandler : MonoBehaviour {

    public QuestObject currentQuest;
    SupplyData gameData;

	// Use this for initialization
	void Start () {
        gameData = transform.gameObject.GetComponent<SupplyData>();
        print(gameData.gameObject.name);
        print(currentQuest.Description);
        print(currentQuest.questDecisions[0].decisionDescription);
        QuestResults(currentQuest.questDecisions[0]);
	}

    void QuestResults(Decision questDecision){
        Branch selectedBranch;
        if(questDecision.branchStyle == BranchStyle.Random) {
            selectedBranch = UsefulExtensions.Pick<Branch>(questDecision.branches);
        } else {
            bool requirementsMet = true;
            foreach (Requirement r in questDecision.requirements){
                requirementsMet = requirementsMet & CompareValue(r.type.ToString(), r.amount);
            }
            if (requirementsMet){
                selectedBranch = questDecision.branches[0];
            } else {
                selectedBranch = questDecision.branches[1];
            }
        }
        foreach(Result r in selectedBranch.results){
            ChangeValue(r.value.ToString(), r.quantity);
        }
        print(selectedBranch.Description);
    }

    public bool CompareValue(string type, int quantity){
        FieldInfo value = gameData.GetType().GetField(type);
        return ((int)value.GetValue(gameData)) >= quantity;
    }

    public void ChangeValue(string type, int quantity){
        FieldInfo value = gameData.GetType().GetField(type);
        value.SetValue(gameData, quantity + (int)value.GetValue(gameData));
    }
}
