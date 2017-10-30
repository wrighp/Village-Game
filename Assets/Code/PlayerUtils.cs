using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUtils : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindObjectOfType<QuestHandler>().player = this;
	}
	
    [Command]
    public void CmdQuestVote(int selection){
        print("CmdQuestVote");
        GameObject.FindObjectOfType<QuestHandler>().votes.Add(selection);
    }
}
