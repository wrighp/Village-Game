using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeadzone : MonoBehaviour {
	
	public float deadzone; //Deadzone in either direction before moving
	public List<Transform> targets;

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
			LineDebug.DrawRay(averagePosition, Vector3.up, Color.red);
		}

		//Push camera if average position moves outside of deadzone
		float x = averagePosition.x - transform.position.x;
		float distance = Mathf.Abs(x);
		if(distance > deadzone){
			float direction = Mathf.Sign(x);
			Vector3 pos = transform.position;
			pos.x = averagePosition.x - deadzone * direction;
			transform.position = pos;
		}

		DrawBoundary();
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
