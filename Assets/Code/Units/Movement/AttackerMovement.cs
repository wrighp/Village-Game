using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (CharacterMovement))]
public class AttackerMovement : NetworkBehaviour {

	public Transform target;
	CharacterMovement movement;
    float cooldown = 5f;

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
            if (Vector2.Distance(target.transform.position, transform.position) < 3 && cooldown <= 0) {
                print("attack");
                cooldown = 5f;
                movement.direction = Vector2.zero;
                GetComponentInChildren<Animator>().Play("SwordSwing_Right");
                Cmds.i.CmdDealDamage(target.gameObject);
            } else if(Vector2.Distance(target.transform.position, transform.position) < 3) {
                movement.direction = Vector2.zero;
               cooldown -= Time.deltaTime;
            } else {
                movement.speedMultiplier = 1;
                movement.direction = target.position - transform.position;
                movement.direction.Normalize();
                cooldown -= Time.deltaTime;
            }
		}
		else{
            AttackSystem atk = GetComponent<AttackSystem>();
            if(atk.faction == UnitAlliance.EnemyFighter)
            {
                var players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length > 0)
                {
                    List<GameObject> targets = new List<GameObject>();
                    targets.AddRange(targets);
                    targets.AddRange(UnitManager.i.friendlyFighters);
                    target = targets.Pick().transform;
                }
            } else {
                if(UnitManager.i.enemyFighters.Count > 0)
                    target = UnitManager.i.enemyFighters.Pick().transform;
            }

		}
	}
}

public partial class Cmds : NetworkBehaviour
{
    [Command]
    public void CmdDealDamage(GameObject unit) {
        unit.GetComponent<AttackSystem>().health -= 2;
    }
}