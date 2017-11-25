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

    public override void OnTurnStart() {
        print("OnTurnStart");
        UIManager.i.SpawnFloatingText("+" + 5 * transform.parent.GetComponent<Tile>().units.Count + " Food", transform.position);
        //Floating Text here for Fighter increase on clients
        if (isServer)
            sD.food += 5 * transform.parent.GetComponent<Tile>().units.Count;
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
        bsObj.transform.Translate(Vector3.back);
        bS.OnBuild();
        Tile t = tile.GetComponent<Tile>();
        foreach (SquadUnit s in t.units){
            s.unit.GetComponent<FollowerMovement>().rooted = true;
        }
        Cmds.i.RpcPrintFloatingText(tile, "Used 5 wood and 5 Gold");
        Cmds.i.RpcBuild(tile, bsObj);
    }
}