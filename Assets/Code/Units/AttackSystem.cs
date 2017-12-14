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
    public int health = 100;
    public float atkRate = 1f;
    public float defMult = 1f;
    public float atkMult = 1f;

    int attackStage = -1; //what part of chain attack attack is on
    public float windupTime = 0;
    public float backswingTime = 0;
    [SyncVar]
    public int bufferedAttack = -1; //Was another attack scheduled to attack during the backswing for attackStage attack

    // Use this for initialization
    void Start() {
        //Spawn weapon prefab in hand
        Transform handTransform = transform.Find("weapon_slot_r");
        if (handTransform != null && weaponObject.weaponPrefab != null) {
            weaponItem = (GameObject)GameObject.Instantiate(weaponObject.weaponPrefab, Vector3.zero, Quaternion.identity, handTransform);
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
    void Update(){
        if (health <= 0) {
            if (!isLocalPlayer)
                UnitManager.i.DestroyUnit(this.gameObject);
            else {
                transform.position = Vector3.zero;
                FightManager.i.inCombatPlayers--;
                CmdSetHeatlth(1000, GetComponent<NetworkIdentity>().netId);
            }
        }
        //Doing a windup
        if (IsInWindup()){
            windupTime -= Time.deltaTime * atkRate;
            //Is windup finished (attack being done now)
            if (windupTime <= 0){
                WeaponCollision wc = GetComponentInChildren<WeaponCollision>();
                List<GameObject> objectsHit = new List<GameObject>();
                foreach (GameObject go in wc.currentCollisions){
                    objectsHit.Add(go);
                }

                int objectsHitCount = objectsHit.Count;

                WeaponAttack chainAttack = weaponObject.chainAttack[attackStage];
                foreach (SwingEffect swing in chainAttack.swingEffects){
                    swing.OnBeforeHit(this, objectsHitCount);
                }

                foreach (SwingEffect swing in chainAttack.swingEffects){
                    if (objectsHit.Count == 0){
                        swing.OnMiss(this);
                    }
                    else {
                        for (int i = 0; i < objectsHitCount && i < chainAttack.cleave; i++){
                            GameObject obj = objectsHit[i];
                            swing.OnHit(this, obj, i, objectsHitCount);
                            //Play random hit sound

                            //Apply base damage
                            if (obj == null) break;
                            AttackSystem at = obj.GetComponent<AttackSystem>();
                            if (at != null && at.faction != faction) {
                                at.CmdInflictDamage((int)(20 * atkMult / at.defMult), at.GetComponent<NetworkIdentity>().netId);
                            }
                        }
                    }
                }
            }
        }
        //Windup was completed
        else {
            //Doing backswing
            if (IsInBackswing()){
                backswingTime -= Time.deltaTime * atkRate;
                //backswing just finished
                if (backswingTime <= 0){
                    WeaponAttack chainAttack = weaponObject.chainAttack[attackStage];
                    foreach (SwingEffect swing in chainAttack.swingEffects){
                        swing.OnComplete(this);
                    }
                    attackStage = -1;
                }
            }
            //Backswing was completed
            //E.g. Doing nothing currently
            else {
                if (bufferedAttack >= 0){
                    BeginAttack();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && isLocalPlayer) {
            print("isLocalplayer");
            CmdAttackMessage(IsInWindup());
        }

    }

    void BeginAttack() {
        WeaponCollision wc = GetComponentInChildren<WeaponCollision>();
        wc.currentCollisions.Clear();
        
        WeaponAttack chainAttack = weaponObject.chainAttack[bufferedAttack];
        windupTime = chainAttack.windupTime;
        backswingTime = chainAttack.backswingTime;

        foreach (SwingEffect swing in chainAttack.swingEffects) {
            swing.IsServer = isServer;
            swing.OnSwing(this);
        }

        Animator anim = GetComponentInChildren<Animator>();
        if (wc == null || wc.type.Equals("sword")) {
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
    public void CmdAttackMessage(bool isInWindup) {
        if (!isInWindup)
        {
            bufferedAttack = attackStage + 1;
        }
    }

    [Command]
    public void CmdInflictDamage(int dmg, NetworkInstanceId netId) {
        GameObject target = ClientScene.FindLocalObject(netId);
        target.GetComponent<AttackSystem>().health -= dmg;
    }

    [Command]
    public void CmdSetHeatlth(int hp, NetworkInstanceId netId) {
        GameObject target = ClientScene.FindLocalObject(netId);
        target.GetComponent<AttackSystem>().health = hp;
    }
}
