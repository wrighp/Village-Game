using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class DecisionOption : ScriptableObject{
    public string[] EndingDescriptions;
    ///returns true if option is visible to player to choose
    public abstract bool IsVisible();
    
    //returns true if player can select the option (like if they can or can't afford to purchase something)
    public abstract bool isSelectable();

    ///Returns next decision branch to take (from branches array), -1 if this ends the chain
    ///Different results can be decided here with basic float value = Random.value and > switches
    public abstract int onChosen();

}
