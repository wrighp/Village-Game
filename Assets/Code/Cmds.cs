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
        if (isServer) {
            foreach(GameObject t in TileManager.instance.tileIDs){
                int val = UnityEngine.Random.Range(0, 100);
                if (val > 90){
                    Cmds.i.CmdBuildRock(t);
                } else if (val > 70){
                    Cmds.i.CmdBuildTree(t);
                } 
            }
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
                if (Home.CanBuild()){
                    i.CmdBuildHome(tile);
                }
                break;
            case 3:
                i.CmdDestroy(tile);
                break;
        }
    }

}