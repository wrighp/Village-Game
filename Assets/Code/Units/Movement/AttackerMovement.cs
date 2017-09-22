using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerMovement : CharacterMovement {

	public Transform target;

	protected override void Awake(){
		base.Awake();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {

		base.Update();
		if(!hasAuthority){
			return;
		}

		if(target != null){
			direction = target.position - transform.position;
			direction.Normalize();
		}
		else{
			var players = GameObject.FindGameObjectsWithTag("Player");
			if(players.Length > 0){
				target = players.Pick().transform;
			}
		}
	}
}
