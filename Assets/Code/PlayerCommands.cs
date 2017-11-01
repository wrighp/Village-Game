using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Networking;

public partial class PlayerCommands : MonoBehaviour{

    static public PlayerCommands cmd;
    static public PlayerUtils playerUtils;

    public void Start() {
        if(cmd == null){
            cmd = this;
        }
    }

    public void AddVote(int vote) {
        playerUtils.CmdQuestVote(vote);
    }

    public void PerformBuild(int selection, GameObject tile) {
        switch (selection)
        {
            case 0:
                if (Blacksmith.CanBuild()){
                    playerUtils.CmdBuildBlacksmith(tile);
                }
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

}