﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Character movement for players and npc's.
/// Movement can be tweaked by editing accelration, maxSpeed, deceleration, and direction of movement.
/// Movement on x and y axes should have separate max speeds (to reduce slugishness)
/// </summary>
public class CharacterMovement : NetworkBehaviour {

	public float acceleration = 75f;
	public float maxSpeed = 5f;
	public float decceleration = 20f;
	public Vector2 direction; //Non-normalized directions act as acceleration multiplier

	[SyncVar(hook = "OnDirectionFacesChange")]
	public bool directionFacesRight = true;

	public Vector2 DirectionFacing {
		get {
			return Vector2.right * (directionFacesRight ? 1f : -1f);
		}
	}

	Rigidbody2D rb;
	// Use this for initialization
	protected virtual void Start () {
		
	}

	public override void OnStartAuthority()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	protected virtual void Update () {
		LineDebug.DrawRay(transform.position + Vector3.back, DirectionFacing * .75f, Color.red);
	}

	protected virtual void FixedUpdate(){
		if(hasAuthority){
			
			if(direction.magnitude <= float.Epsilon){
				rb.drag = decceleration;
			}
			else{
				rb.drag = 0f;
			}

			rb.AddForce(direction * acceleration, ForceMode2D.Force);
			if(rb.velocity.magnitude > maxSpeed){
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}

			//Set facing direction to desired X direction
			//In the future it may be possible to move backwards without turning however
			//Can be based on direction instead of velocity
			if (System.Math.Abs (rb.velocity.x) > float.Epsilon) {
				bool right = rb.velocity.x > 0;
				directionFacesRight = right;
				CmdSetFacingDirection (right);
			}
			direction = Vector2.zero;
		}
	}

	/// <summary>
	/// Send unreliably as it is set every frame
	/// </summary>
	/// <param name="right">If set to <c>true</c> right.</param>
	[Command (channel = 1)]
	void CmdSetFacingDirection(bool right){
		directionFacesRight = right;
	}

	void OnDirectionFacesChange(bool directoinFacesRightNew){
		directionFacesRight = directoinFacesRightNew;
	}
}
