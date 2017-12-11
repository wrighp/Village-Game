using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FightManager : NetworkBehaviour {

    public static FightManager i;

    public bool inCombat = false;

    FightManager()
    {
        if (i == null){
            i = this;
        }
    }

    [Server]
    public void StartFight(int enemies) {
        inCombat = true;
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            player.transform.position = new Vector3(52, 3, 0);
        }
        SupplyData s = GameObject.FindObjectOfType<SupplyData>();
        for (int i = 0; i < s.fighters; ++i) {
            UnitManager.i.SpawnUnit(new Vector3(52, 6, 0), UnitAlliance.FriendlyFighter, Resources.Load<GameObject>("NetworkPrefabs/Attacker"));
        }
        for (int i = 0; i < enemies; ++i) {
            UnitManager.i.SpawnUnit(new Vector3(65, 4, 0), UnitAlliance.EnemyFighter, Resources.Load<GameObject>("NetworkPrefabs/Attacker"));
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
        }
	}
}
