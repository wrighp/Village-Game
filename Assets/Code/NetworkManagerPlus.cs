﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerPlus : NetworkManager {

    public NetworkConnection netCon = null;

	//Code here for Editor only.
	#if UNITY_EDITOR
	public void RegisterPrefabs(List<GameObject> objects)
	{
		int previousCount = this.spawnPrefabs.Count;
		this.spawnPrefabs.Clear();
		this.spawnPrefabs.AddRange(objects);
		Debug.LogFormat("Assigned {0} prefab(s) to spawn list, previously held {1} prefab(s).",this.spawnPrefabs.Count, previousCount);
	}
	#endif

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
        if(netCon == null) {
            netCon = conn;
        }
		base.OnServerAddPlayer (conn, playerControllerId);
		//Network.connections.Length;
		Debug.Log("Player " + conn.connectionId + " connected from " + conn.address);
	}

}
