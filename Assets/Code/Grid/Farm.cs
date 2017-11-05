using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Farm : Building {

    void Start() {
        isObstruction = false;
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Buildings/Farm");
        GameObject parent = ClientScene.FindLocalObject(parentId);
        parent.GetComponent<Tile>().building = this;
        transform.parent = parent.transform;
    }

    new public static bool CanBuild() {
        SupplyData sData = GameObject.FindObjectOfType<SupplyData>();
        return sData.wood > 5 && sData.gold > 5;
    }

    public override void OnBuild() {
        sD = GameObject.FindObjectOfType<SupplyData>();
        sD.wood -= 5;
        sD.gold -= 5;
    }

    public override void OnRemove(){
        sD.wood += 3;
    }

    public override void OnTurnEnd(){
        print("OnTurnEnd");
    }

    public override void OnTurnStart(){
        print("OnTurnStart");
        //Floating Text here for Fighter increase on clients
        if (isServer)
            sD.food += 5 * gameObject.GetComponent<Tile>().units.Count;
    }

    public override void OnInteract(){
    }
}

public partial class Cmds : NetworkBehaviour {
	[Command]
    public void CmdBuildFarm(GameObject tile) {
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Farm"), tile.transform.position, Quaternion.identity);
        Farm bS = bsObj.GetComponent<Farm>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        bS.OnBuild();
        Cmds.i.RpcBuild(tile, bsObj);
    }
}