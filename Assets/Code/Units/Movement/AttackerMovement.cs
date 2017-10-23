using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (CharacterMovement))]
public class AttackerMovement : NetworkBehaviour {

	public Transform target;
	CharacterMovement movement;

	void Awake(){
		movement = GetComponent<CharacterMovement>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasAuthority){
			return;
		}

		if(target != null){
			movement.direction = target.position - transform.position;
			movement.direction.Normalize();
		}
		else{
			var players = GameObject.FindGameObjectsWithTag("Player");
			if(players.Length > 0){
				target = players.Pick().transform;
			}
		}
	}
}
