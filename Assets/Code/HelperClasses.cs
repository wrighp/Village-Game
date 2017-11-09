using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncListNetworkInstanceId : SyncList<NetworkInstanceId> {
	#region implemented abstract members of SyncList

	protected override void SerializeItem (NetworkWriter writer, NetworkInstanceId item)
	{
		writer.Write(item);
	}

	protected override NetworkInstanceId DeserializeItem (NetworkReader reader)
	{
		return reader.ReadNetworkId();
	}

	#endregion
 }
	
public class SyncListGameObject : SyncList<GameObject> {
	#region implemented abstract members of SyncList

	protected override void SerializeItem (NetworkWriter writer, GameObject item)
	{
		writer.Write(item);
	}

	protected override GameObject DeserializeItem (NetworkReader reader)
	{
		return reader.ReadGameObject();
	}

	#endregion
 }

public class SyncListNetworkIdentity : SyncList<NetworkIdentity> {
	#region implemented abstract members of SyncList

	protected override void SerializeItem (NetworkWriter writer, NetworkIdentity item)
	{
		writer.Write(item);
	}

	protected override NetworkIdentity DeserializeItem (NetworkReader reader)
	{
		return reader.ReadNetworkIdentity();
	}

	#endregion
 }
	