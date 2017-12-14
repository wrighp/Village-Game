using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LetMerchantEvent", menuName = "QuestDecisions/LetMerchantEvent", order = 1)]
public class LetMerchantsIn : DecisionOption {

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
        SupplyData sD = GameObject.FindObjectOfType<SupplyData>();
        if (sD.gold < 5) {
            int select = Random.Range(0,99);
            if (select > 75) {
                GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[0];
                sD.gold = Mathf.Clamp(sD.gold, 0, sD.gold - 5);
                sD.food = Mathf.Clamp(sD.food, 0, sD.food + 15);
            } else if (select > 10) {
                GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[1];
                sD.gold = Mathf.Clamp(sD.gold, 0, sD.gold - 5);
                sD.food = Mathf.Clamp(sD.food, 0, sD.food + 8);
            } else {
                GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[2];
                sD.gold = Mathf.Clamp(sD.gold, 0, sD.gold - 5);
            }
        } else {
            GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[3];
        }
        return -1;
    }

}
