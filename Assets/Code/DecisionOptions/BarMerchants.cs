using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
[CreateAssetMenu(fileName = "BarMerchantEvent", menuName = "QuestDecisions/BarMerchantEvent", order = 1)]
public class BarMerchants : DecisionOption {

    ///returns true if option is visible to player to choose
    public override bool IsVisible() {
        return true;
    }
    
    //returns true if player can select the option (like if they can or can't afford to purchase something)
    public override bool isSelectable() {
        return true;
    }

    ///Returns next decision branch to take (from branches array), -1 if this ends the chain
    ///Different results can be decided here with basic float value = Random.value and > switches
    public override int onChosen() {
        int randVal = Random.Range(0, 99);
        if (randVal > 98){
            GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[0];
            return 0;
        } else if (randVal > 50){
            GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[1];
            return -1;
        } else {
            GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[2];
            return 0;
        }
    }

}
