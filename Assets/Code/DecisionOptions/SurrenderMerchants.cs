using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SurrenderMerchantEvent", menuName = "QuestDecisions/SurrenderMerchantEvent", order = 1)]
public class SurrenderMerchants : DecisionOption {

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
        GameObject.FindObjectOfType<QuestHandler>().eventDescription.text = EndingDescriptions[0];
        SupplyData sD = GameObject.FindObjectOfType<SupplyData>();
        sD.gold = Mathf.Clamp(sD.gold, 0, sD.gold - 5);
        sD.food = Mathf.Clamp(sD.food, 0, sD.food - 5);
        return -1;
    }

}
