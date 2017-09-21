using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCamera : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Adds player objects to camera view on connection, for multiple local players
	/// </summary>
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		Camera.main.GetComponent<CameraDeadzone>().AddPlayerTargets();
	}
}
