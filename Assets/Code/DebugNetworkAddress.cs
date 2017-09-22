using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebugNetworkAddress : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		Debug.LogFormat("External IP: {0}\nExternal port: {1}\nIP: {2}\nPort: {3}\n",Network.player.externalIP,Network.player.externalPort, Network.player.ipAddress, Network.player.port);
	}
	public override void OnStartServer(){
		Debug.LogFormat("Bound to IP: " + NetworkManager.singleton.networkAddress + " on Port: " + NetworkManager.singleton.networkPort);
	}
}
