using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer || !hasAuthority){
			return;
		}
		//Make sure to use Raw Axis or movement becomes very lethargics
		direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		direction.Normalize();
	}
}
