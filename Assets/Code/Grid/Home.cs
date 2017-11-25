using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Home : Building {

    void Start() {
        isObstruction = false;
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Buildings/House");
        GameObject parent = ClientScene.FindLocalObject(parentId);
        parent.GetComponent<Tile>().building = this;
        transform.parent = parent.transform;
    }

    new public static bool CanBuild() {
        SupplyData sData = GameObject.FindObjectOfType<SupplyData>();
        return sData.wood > 5 && sData.gold > 2;
    }

    public override void OnBuild() {
        sD = GameObject.FindObjectOfType<SupplyData>();
        sD.wood -= 5;
        sD.gold -= 2;
    }

    public override void OnRemove(){
        sD.wood += 3;
    }

    public override void OnTurnEnd(){
        print("OnTurnEnd");
    }

    public override void OnTurnStart(){
        print("OnTurnStart");
    }

    public override void OnInteract(){
    }
}

public partial class Cmds : NetworkBehaviour {
	[Command]
    public void CmdBuildHome(GameObject tile) {
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Home"), tile.transform.position, Quaternion.identity);
        Home bS = bsObj.GetComponent<Home>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        bsObj.transform.Translate(Vector3.back);
        bS.OnBuild();
        Tile t = tile.GetComponent<Tile>();
        foreach (SquadUnit s in t.units) {
            s.unit.GetComponent<FollowerMovement>().rooted = true;
        }
        Cmds.i.RpcBuild(tile, bsObj);
    }
}