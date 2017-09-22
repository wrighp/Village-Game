using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraDeadzone : NetworkBehaviour {
	
	public float deadzone; //Deadzone in either direction before moving
	public List<Transform> targets;
	public float moveSpeed = 2f;


	//private float modifiedDeadzone;
	bool selected;
	void Awake(){
		targets = new List<Transform>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(targets.Count == 0){
			DrawBoundary();
			return;
		}

		//Get average positions of all transforms
		Vector3 averagePosition = new Vector3();
		for (int i = 0, targetsCount = targets.Count; i < targetsCount; i++) {
			averagePosition += targets [i].position;
		}
		averagePosition *= 1f/targets.Count;

		//Get max and min x to set camera projection size if necessary
		//
		//

		if(selected){
			Vector3 zPosition = averagePosition;
			zPosition.z = transform.position.z + .5f;
			LineDebug.DrawRay(zPosition + Vector3.down * .5f, Vector3.up, Color.blue);
			LineDebug.DrawRay(zPosition + Vector3.left * .5f, Vector3.right, Color.blue);

		}

		//Push camera if average position moves outside of deadzone
		float x = averagePosition.x - transform.position.x;
		float distance = Mathf.Abs(x);
		if(distance > deadzone){
			float direction = Mathf.Sign(x);
			Vector3 pos = transform.position;
			pos.x = averagePosition.x - deadzone * direction;

			//transform.position = pos;
			transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * moveSpeed);
		}

		DrawBoundary();
	}
		
	/// <summary>
	/// Adds player to camera targets.
	/// Should be called when new player is added, or new player connected
	/// </summary>
	public void AddPlayerTarget(Transform player){
		targets.Add(player);
	}

	public void RemovePlayerTarget(Transform player){
		targets.Remove(player);
	}


	void DrawBoundary(){
		if(!selected){
			return;
		}
		Color color = Color.red;
		LineDebug.DrawRay(transform.position + new Vector3(deadzone, -5f , 1f), Vector3.up * 10f, color);
		LineDebug.DrawRay(transform.position + new Vector3(-deadzone, -5f , 1f), Vector3.up * 10f, color);
		selected = false;
	}
	void OnDrawGizmosSelected(){
		selected = true;
	}

}
