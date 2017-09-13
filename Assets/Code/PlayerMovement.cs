using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public float moveForce = 1f;
	Vector2 direction;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartLocalPlayer()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer){
			return;
		}
		direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		direction.Normalize();
	}

	void FixedUpdate(){
		rb.AddForce(direction * moveForce, ForceMode2D.Force);
	}
}
