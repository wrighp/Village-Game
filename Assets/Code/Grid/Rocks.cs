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

        Tile tile = transform.parent.GetComponent<Tile>();
        if (tile.units.Count != 0){
            //Floating Text here for stone increase on clients
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (isServer) {
                sD.stone += 10;
            }
            UIManager.i.SpawnFloatingText("+10 Stone", transform.position);
            tile.building = null;
            Destroy(this);
        }
    }
}

public partial class Cmds : NetworkBehaviour {
    [Command]
    public void CmdBuildRock(GameObject tile) {
        GameObject bsObj = Instantiate(Resources.Load<GameObject>("NetworkPrefabs/Rocks"), tile.transform.position, Quaternion.identity);
        Rocks bS = bsObj.GetComponent<Rocks>();
        NetworkServer.Spawn(bsObj);
        bS.parentId = tile.GetComponent<NetworkIdentity>().netId;
        bsObj.transform.parent = tile.transform;
        bsObj.transform.Translate(Vector3.back);
        bS.OnBuild();
        Cmds.i.RpcBuild(tile, bsObj);
    }
}