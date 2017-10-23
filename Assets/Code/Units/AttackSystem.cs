﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

/// <summary>
/// Script to handle weapon attacking and swings.
/// </summary>
public class AttackSystem : NetworkBehaviour {

	public WeaponObject weaponObject;

	GameObject weaponItem;

	int attackStage = -1; //what part of chain attack attack is on
	float windupTime = 0;
	float backswingTime = 0;
	[SyncVar] int bufferedAttack = -1; //Was another attack scheduled to attack during the backswing for attackStage attack

	// Use this for initialization
	void Start () {
		//Spawn weapon prefab in hand
		Transform handTransform = transform.Find("weapon_slot_r");
		if(handTransform != null && weaponObject.weaponPrefab != null){
			weaponItem = (GameObject)GameObject.Instantiate(weaponObject.weaponPrefab,Vector3.zero,Quaternion.identity,handTransform);
		}
	}

	public bool IsInWindup(){
		return windupTime > 0;
	}

	public bool IsInBackswing(){
		return backswingTime > 0;
	}

	public bool IsAttacking(){
		return backswingTime > 0;
	}

	// Update is called once per frame
	void Update () {

		//Doing a windup
		if(IsInWindup()){
			windupTime -= Time.deltaTime;
			//Is windup finished (attack being done now)
			if(windupTime <= 0){
				//Check for hits on things, get list of hit objects
				//
				//
				//
				//
				List<GameObject> objectsHit = new List<GameObject>();
				int objectsHitCount = objectsHit.Count;
				WeaponAttack chainAttack = weaponObject.chainAttack[attackStage];
				foreach(SwingEffect swing in chainAttack.swingEffects){
					swing.OnBeforeHit(this, objectsHitCount);
				}

				foreach(SwingEffect swing in chainAttack.swingEffects){
					if(objectsHit.Count == 0){
						swing.OnMiss(this);
					}
					else{
						for (int i = 0; i < objectsHitCount && i < chainAttack.cleave; i++) {
							GameObject obj = objectsHit [i];
							swing.OnHit(this, obj, i, objectsHitCount);
							//Play random hit sound
							//
							//
							//
							//

							//Apply base damage
							//
							//
							//
							//
						}
					}
				}
			}
		}
		//Windup was completed
		else{
			//Doing backswing
			if(IsInBackswing()){
				backswingTime -= Time.deltaTime;
				//backswing just finished
				if(backswingTime <= 0){
					WeaponAttack chainAttack = weaponObject.chainAttack[attackStage];
					foreach(SwingEffect swing in chainAttack.swingEffects){
						swing.OnComplete(this);
					}
					attackStage = -1;
				}
			}
			//Backswing was completed
			//E.g. Doing nothing currently
			else{
				if(bufferedAttack >= 0){
					BeginAttack();
				}
				else{
					
				}
			}
		}

	}

	void BeginAttack(){
		WeaponAttack chainAttack = weaponObject.chainAttack[bufferedAttack];
		windupTime = chainAttack.windupTime;
		backswingTime = chainAttack.backswingTime;

		foreach(SwingEffect swing in chainAttack.swingEffects){
			swing.IsServer = isServer;
			swing.OnSwing(this);
		}

		//Play animation on weaponItem or character
		//
		//
		//

		//Play random swing sound
		//
		//
		//

		attackStage = bufferedAttack;
		//Client and server reset value at same time
		bufferedAttack = -1;


	}

	//Send attack message (like after button press to attack)
	[Command]
	public void CmdAttackMessage(){
		if(!IsInWindup()){
			bufferedAttack = attackStage + 1;
		}
	}

}
