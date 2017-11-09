using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Trees : Building {

    void Start(){
        isObstruction = true;
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Environment/SpringTree");
        GameObject parent = ClientScene.FindLocalObject(parentId);
        parent.GetComponent<Tile>().building = this;
        transform.parent = parent.transform;
    }

    public override void OnBuild() {}

    public override void OnInteract() {}

    public override void OnRemove() {}

    public override void OnTurnEnd() {}

    public override void OnTurnStart() {
        Tile tile = transform.parent.GetComponent<Tile>();
        if (tile.units.Count != 0){
            //Floating Text here for stone increase on clients
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (isServer) {
                sD.wood += 10;
            }
            tile.building = null;
            UIManager.i.SpawnFloatingText("+10 Wood", transform.position);
            Destroy(this);
        }
    }
}

public partial class Cmds : NetworkBehaviour {
    [Command]
    public void CmdBuildTree(GameObject tile) {
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Tree"), tile.transform.position, Quaternion.identity);
        //bsObj.transform.Translate(Vector3.back);
        Trees bS = bsObj.GetComponent<Trees>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        //bsObj.transform.Translate(Vector3.back);
        bS.OnBuild();
        Cmds.i.RpcBuild(tile, bsObj);
    }
}