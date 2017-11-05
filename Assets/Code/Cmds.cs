using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Networking;

public partial class Cmds : NetworkBehaviour{

    static public Cmds i;

	public void Awake(){
		

	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartClient ();
		if(isLocalPlayer){
			i = this;
		}
	}

    public void Start() {
        
    }

    public void AddVote(int vote) {
        i.CmdQuestVote(vote);
    }

    public void PerformBuild(int selection, GameObject tile) {
        Tile t = tile.GetComponent<Tile>();
        if (t.units.Count == 0){
            return;
        }
        switch (selection)
        {
            case 0:
                if (Blacksmith.CanBuild()){
                    i.CmdBuildBlacksmith(tile);
                }
                break;
            case 1:
                if (Farm.CanBuild()) {
                    i.CmdBuildFarm(tile);
                }
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

}