using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Character movement for players and npc's.
/// Movement can be tweaked by editing accelration, maxSpeed, deceleration, and direction of movement.
/// </summary>
public class CharacterMovement : NetworkBehaviour {

	public float acceleration = 75f;
	public float maxSpeed = 5f;
	public float decceleration = 20f;
	public Vector2 direction;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartAuthority()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
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
			direction = Vector2.zero;
		}
	}
}
