using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Rocks : Building {

    void Start(){
        isObstruction = true;
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Environment/RockCluster");
        GameObject parent = ClientScene.FindLocalObject(parentId);
        parent.GetComponent<Tile>().building = this;
        transform.parent = parent.transform;
    }

    public override void OnBuild() {}

    public override void OnInteract() {}

    public override void OnRemove() {}

    public override void OnTurnEnd() {}

    public override void OnTurnStart() {

        Tile tile = gameObject.GetComponent<Tile>();
        if (tile.units.Count != 0){
            //Floating Text here for stone increase on clients
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (isServer) {
                sD.stone += 10;
            }
            Destroy(this);
        }
    }
}

public partial class Cmds : NetworkBehaviour {
    [Command]
    public void CmdBuildRock(GameObject tile) {
        //RpcBuildBlacksmith(tile);
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Rocks"), tile.transform.position, Quaternion.identity);
        Blacksmith bS = bsObj.GetComponent<Blacksmith>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        bS.OnBuild();
        Cmds.i.RpcBuild(tile, bsObj);
    }
}