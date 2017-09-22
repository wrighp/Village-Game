using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Follower will move towards target if it is more than the min distance away from it
/// </summary>
public class FollowerMovement : CharacterMovement {

	public float minDistance = 4f;
	public Transform target;
	// Use this for initialization
	protected override void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		if(!hasAuthority){
			return;
		}
		//Make sure to use Raw Axis or movement becomes very lethargic
		direction = target.position - transform.position;
		float dist = direction.magnitude;
		if(dist > minDistance){
			direction.Normalize();
		}
		else{
			direction = Vector2.zero;
		}

	}
}
