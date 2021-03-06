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

	[SyncVar]public float acceleration = 75f;
	[SyncVar]public float maxSpeed = 5f;
	[SyncVar]public float decceleration = 20f;
	[SyncVar]public Vector2 direction; //Non-normalized directions act as acceleration multiplier
	[SyncVar]public float speedMultiplier = 1f;

	[SyncVar(hook = "OnDirectionFacesChange")]
	public bool directionFacesRight = true;

	public  Transform bodyTransform;
	public Animator animator;

	public Vector2 DirectionFacing {
		get {
			return Vector2.right * (directionFacesRight ? 1f : -1f);
		}
	}

	Rigidbody2D rb;
	protected virtual void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	protected virtual void Start () {
		
	}

	public override void OnStartAuthority()
	{
		
	}
	// Update is called once per frame
	protected virtual void Update () {
		PlayerDebug.DrawRay(transform.position + Vector3.back, DirectionFacing * .75f, new Color(1f,0,0,1f));
		if(animator != null){
			animator.SetFloat("MoveSpeed",rb.velocity.magnitude);
			animator.SetBool("Stopping", direction.magnitude == 0);
		}
	}

	protected virtual void FixedUpdate(){
		if(hasAuthority && rb.simulated){
			//Send direction to all clients
			CmdSetDirection(direction);

			//Set facing direction to desired X direction
			//In the future it may be possible to move backwards without turning however
			//Can be based on direction instead of velocity
			if (System.Math.Abs (rb.velocity.x) > float.Epsilon) {
				bool right = rb.velocity.x > 0;
				directionFacesRight = right;
				CmdSetFacingDirection (right);
			}
		}
		if(direction.magnitude <= float.Epsilon){
			if(rb.velocity.magnitude > 10f / decceleration){ //.05 acting as epsilon to prevent hysteresis
				rb.AddForce(-rb.velocity.normalized * decceleration * speedMultiplier, ForceMode2D.Force);
			}
		}
		else{
			rb.AddForce(direction * acceleration * speedMultiplier, ForceMode2D.Impulse);
		}
			
		if(rb.velocity.magnitude > maxSpeed * speedMultiplier){
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		//direction = Vector2.zero;
	}

	/// <summary>
	/// Send unreliably as it is set every frame
	/// </summary>
	/// <param name="right">If set to <c>true</c> right.</param>
	[Command (channel = 3)]
	void CmdSetFacingDirection(bool right){
		directionFacesRight = right;
	}

	void OnDirectionFacesChange(bool directionFacesRightNew){
		directionFacesRight = directionFacesRightNew;

		Vector3 scale = bodyTransform.localScale;
		float flipDir = directionFacesRight ? 1f : -1f;
		bodyTransform.localScale = new Vector3(Mathf.Abs(scale.x) * flipDir, scale.y,scale.z);
	}

	[Command (channel = 3)]
	protected void CmdSetDirection(Vector3 dir){
		direction = dir;
	}

	[Command (channel = 3)]
	public void CmdSetSpeedMultiplier(float speed){
		speedMultiplier = speed;
	}


	public void SetBody (GameObject go)
	{
		bodyTransform = go.transform;
		animator = bodyTransform.GetComponent<Animator>();
	}
}
