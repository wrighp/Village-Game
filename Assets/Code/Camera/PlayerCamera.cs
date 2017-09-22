using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCamera : NetworkBehaviour {

	void Awake(){
		
	}
	void Start(){

	}
	// Use this for initialization
	void OnEnable () {
		Camera.main.GetComponent<CameraDeadzone>().AddPlayerTarget(transform);
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
		//Camera.main.GetComponent<CameraDeadzone>().AddPlayerTarget();
	}

	/// <summary>
	/// Called to bypass the OnDisable event in the case of quitting (prevents errors).
	/// </summary>
	void OnApplicationQuit(){
		Destroy(this);
	}

	void OnDisable(){
		if(Camera.main != null){
			Camera.main.GetComponent<CameraDeadzone>().RemovePlayerTarget(transform);
		}
	}
}
