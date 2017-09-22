using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement {

	// Use this for initialization
	protected override void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		if(!isLocalPlayer || !hasAuthority){
			return;
		}
		//Make sure to use Raw Axis or movement becomes very lethargics
		direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		direction.Normalize();
	}
}
