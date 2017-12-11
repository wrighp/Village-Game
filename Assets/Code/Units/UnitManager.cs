using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum UnitAlliance{ FriendlyVillager, FriendlyFighter, EnemyFighter };
public class UnitManager : NetworkBehaviour {
	public GameObject unitPrefab;

	public SyncListGameObject friendlyFighters = new SyncListGameObject();
	public SyncListGameObject friendlyVillagers = new SyncListGameObject();
	public SyncListGameObject friendlyUnits = new SyncListGameObject();
	public SyncListGameObject enemyFighters = new SyncListGameObject();
	public SyncListGameObject allUnits = new SyncListGameObject();

	static public UnitManager i;

	void Awake(){
		i = this;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnUnit(new Vector3(5, 5, 0), UnitAlliance.EnemyFighter, Resources.Load<GameObject>("NetworkPrefabs/Attacker"));
        }
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();

		friendlyFighters.Clear();
		friendlyVillagers.Clear();
		friendlyUnits.Clear();
		enemyFighters.Clear();
		allUnits.Clear();

	}

	public void SpawnUnit(Vector3 position, UnitAlliance alliance, GameObject prefab = null){
		Cmds.i.CmdSpawnUnit(position, prefab ?? unitPrefab, alliance);
	}

	public void returnSpawnUnit(GameObject go, UnitAlliance alliance){
		switch (alliance) {
		case UnitAlliance.FriendlyVillager:
			friendlyVillagers.Add(go);
			friendlyUnits.Add(go);
			allUnits.Add(go);
			break;
		case UnitAlliance.FriendlyFighter:
			friendlyFighters.Add(go);
			friendlyUnits.Add(go);
			allUnits.Add(go);
			break;
		case UnitAlliance.EnemyFighter:
			enemyFighters.Add(go);
			allUnits.Add(go);
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
	}

	public void DestroyUnit(GameObject unit){
		friendlyFighters.Remove(unit);
		friendlyVillagers.Remove(unit);
		friendlyUnits.Remove(unit);
		enemyFighters.Remove(unit);
		allUnits.Remove(unit);
	}

}

public partial class Cmds : NetworkBehaviour {
	[Command]
	public void CmdSpawnUnit(Vector3 position, GameObject netPrefab, UnitAlliance alliance) {
		GameObject go = GameObject.Instantiate (netPrefab, position, Quaternion.identity);
		NetworkServer.Spawn(go);

		UnitManager.i.returnSpawnUnit(go,alliance);

	}
}