using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (CharacterMovement))]
public class PlayerMovement : NetworkBehaviour {

	CharacterMovement movement;

	void Awake(){
		movement = GetComponent<CharacterMovement>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (QuestHandler.i.isVoting) return;
		if(!isLocalPlayer /*|| !hasAuthority*/){
			return;
		}
		//Make sure to use Raw Axis or movement becomes very lethargics
		movement.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		//Non-normalized directions act as acceleration multiplier
		movement.direction.Normalize();

	}

}
