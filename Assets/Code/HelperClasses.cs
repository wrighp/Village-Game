using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncListNetworkInstanceId : SyncListStruct<SyncNetID> { }

public struct SyncNetID
{
	public NetworkInstanceId netID;

	public SyncNetID(NetworkInstanceId netID){
		this.netID = netID;
	}
}

public class SyncListGameObject : SyncListStruct<SyncGameObject> { }

public struct SyncGameObject
{
	public GameObject gameObject;

	public SyncGameObject(GameObject gameObject){
		this.gameObject = gameObject;
	}

}

public class SyncListNetworkIdentity : SyncListStruct<SyncNetworkIdentity> { }

public struct SyncNetworkIdentity
{
	public NetworkIdentity networkIdentity;

	public SyncNetworkIdentity(NetworkIdentity networkIdentity){
		this.networkIdentity = networkIdentity;
	}
}