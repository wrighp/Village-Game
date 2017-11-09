using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Building : NetworkBehaviour {
    protected SupplyData sD;
    public bool isObstruction = true;
    [SyncVar]
    public NetworkInstanceId parentId;
    static public bool CanBuild(){
        return true;
    }

    abstract public void OnBuild();

    abstract public void OnRemove();

    abstract public void OnInteract();

    abstract public void OnTurnStart();

    abstract public void OnTurnEnd();

}

public partial class Cmds{
    [ClientRpc]
    public void RpcBuild(GameObject tile, GameObject building) {
        tile.GetComponent<Tile>().building = building.GetComponent<Building>();
        building.transform.parent = tile.transform;

    }

    [Command]
    public void CmdDestroy(GameObject tile){
        tile.GetComponent<Tile>().building.OnRemove();
        i.RpcDestroy(tile);
    }

    [ClientRpc]
    public void RpcDestroy(GameObject tile){
        print(tile.name);
        Tile t = tile.GetComponent<Tile>();
        Destroy(t.building.gameObject);
        t.building = null;
    }

}