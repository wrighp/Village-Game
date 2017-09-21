using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour {

	public static ServerManager instance;

	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// Raises the player connected event. Isn't ever called and was supposedly deprecated
	/// </summary>
	/// <param name="player">Player.</param>
	void OnPlayerConnected(NetworkPlayer player) {
		//Debug.Log("Player " + Network.connections.Length + " connected from " + player.ipAddress + ":" + player.port);
	}

}
