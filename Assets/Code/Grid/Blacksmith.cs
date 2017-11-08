using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Blacksmith : Building {

    void Start() {
        isObstruction = false;
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Buildings/Blacksmith_Building");
        GameObject parent = ClientScene.FindLocalObject(parentId);
        parent.GetComponent<Tile>().building = this;
        transform.parent = parent.transform;
    }

    new public static bool CanBuild() {
        SupplyData sData = GameObject.FindObjectOfType<SupplyData>();
        return sData.wood > 5 && sData.stone > 5;
    }

    public override void OnBuild() {
        sD = GameObject.FindObjectOfType<SupplyData>();
        sD.wood -= 5;
        sD.stone -= 5;
    }

    public override void OnRemove(){
        sD.wood += 2;
        sD.stone += 2;
    }

    public override void OnTurnEnd(){
        print("OnTurnEnd");
    }

    public override void OnTurnStart(){
        print("OnTurnStart");
        //Floating Text here for Fighter increase on clients
        if (isServer)
            sD.fighters += 1 * transform.parent.GetComponent<Tile>().units.Count;
    }

    public override void OnInteract(){
    }
}

public partial class Cmds : NetworkBehaviour {
	[Command]
    public void CmdBuildBlacksmith(GameObject tile) {
        //RpcBuildBlacksmith(tile);
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Blacksmith"), tile.transform.position, Quaternion.identity);
        Blacksmith bS = bsObj.GetComponent<Blacksmith>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        bsObj.transform.Translate(Vector3.back);
        bS.OnBuild();
        Cmds.i.RpcBuild(tile, bsObj);
    }
}