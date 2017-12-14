using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FightManager : NetworkBehaviour {

    public static FightManager i;
    public int inCombatPlayers = 0;
    public bool inCombat = false;

    FightManager() {
        if (i == null){
            i = this;
        }
    }

    [Server]
    public void StartFight(int enemies) {
        inCombatPlayers = 0;
        inCombat = true;
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            player.transform.position = new Vector3(55, 3, 0);
            player.GetComponent<AttackSystem>().health = 1000;
            inCombatPlayers++;
        }
        SupplyData s = GameObject.FindObjectOfType<SupplyData>();
        for (int i = 0; s.fighters>0 && i < 5; ++i) {
            UnitManager.i.SpawnUnit(new Vector3(52, 1.5f*i, 0), UnitAlliance.FriendlyFighter, 0, Resources.Load<GameObject>("NetworkPrefabs/Attacker"));
            s.fighters--; //The numer displayed is the reserve of soldiers, hence the removal
        }
        for (int i = 0; i < enemies; ++i) {
            UnitManager.i.SpawnUnit(new Vector3(65, 1.5f*i, 0), UnitAlliance.EnemyFighter, 1, Resources.Load<GameObject>("NetworkPrefabs/Attacker"));
        }
    }

	// Update is called once per frame
	void Update () {
        if (inCombat && UnitManager.i.enemyFighters.Count == 0 && isServer) {
            inCombat = false;
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players) {
                player.transform.position = Vector3.zero;
            }
            foreach(AttackerMovement fighter in GameObject.FindObjectsOfType<AttackerMovement>()) {
                UnitManager.i.DestroyUnit(fighter.gameObject);
                SupplyData sd = GameObject.FindObjectOfType<SupplyData>();
                sd.fighters++;
            }
        } else if(inCombat && inCombatPlayers <=0) {
            Network.Disconnect();
        }
	}
}
