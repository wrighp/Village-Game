using System.Collections;
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

    public UnitAlliance faction = 0;
    [SyncVar]
    public int health = 2;

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
        if(health <= 0)
        {
            UnitManager.i.DestroyUnit(this.gameObject);
        }
		//Doing a windup
		if(IsInWindup()){
            print("Windup");
			windupTime -= Time.deltaTime;
			//Is windup finished (attack being done now)
			if(windupTime <= 0){
                print("Performing attack");
                WeaponCollision wc = GetComponentInChildren<WeaponCollision>();
                List<GameObject> objectsHit = new List<GameObject>();
                foreach(GameObject go in wc.currentCollisions){
                    objectsHit.Add(go);
                }
                
				int objectsHitCount = objectsHit.Count;
                print(objectsHitCount);
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
                            AttackSystem at = obj.GetComponent<AttackSystem>();
                            if(at != null && at.faction != faction){
                                print(at.gameObject.name);
                                at.CmdInflictDamage(2, at.GetComponent<NetworkIdentity>().netId);
                            }
						}
					}
				}
			}
		}
		//Windup was completed
		else{
            //Doing backswing
            if (IsInBackswing()){
                print("Backswing");
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
                if (bufferedAttack >= 0 ){
                    BeginAttack();
				}
				else
                {

                }
			}
		}

	}

	void BeginAttack(){
        WeaponCollision wc = GetComponentInChildren<WeaponCollision>();
        wc.currentCollisions.Clear();
        
		WeaponAttack chainAttack = weaponObject.chainAttack[bufferedAttack];
		windupTime = chainAttack.windupTime;
		backswingTime = chainAttack.backswingTime;

		foreach(SwingEffect swing in chainAttack.swingEffects){
			swing.IsServer = isServer;
			swing.OnSwing(this);
		}

        Animator anim = GetComponentInChildren<Animator>();
        if(wc == null || wc.type.Equals("sword")) {
            anim.Play("SwordSwing_Right");
        } else {
            anim.Play("WeaponStab_Right");
        }

        //Play random swing sound
        //
        //
        //

        attackStage = 0;
		//Client and server reset value at same time
		bufferedAttack = -1;


	}

	//Send attack message (like after button press to attack)
	[Command]
	public void CmdAttackMessage(){
		if(!IsInWindup()){
            bufferedAttack = attackStage + 1;
            print("Buffered stage: " + bufferedAttack);
		}
	}

    [Command]
    public void CmdInflictDamage(int dmg, NetworkInstanceId netId) {
        GameObject target = ClientScene.FindLocalObject(netId);
        health -= dmg;
    }
}
