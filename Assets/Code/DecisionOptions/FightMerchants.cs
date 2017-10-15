﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "FightMerchantEvent", menuName = "QuestDecisions/FightMerchantEvent", order = 1)]
public class FightMerchants : DecisionOption {

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
        return -1;
    }

}
