using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public struct SquadUnit{
	public GameObject unit;
	public FollowerMovement follower;
	public SquadUnit(GameObject unit, FollowerMovement follower){
		this.unit = unit;
		this.follower = follower;
	}

	public static SquadUnit GameObjectToSquadUnit(GameObject go){
		return new SquadUnit(go, go.GetComponent<FollowerMovement>());
	}
}
public class SyncListSquadUnit : SyncListStruct<SquadUnit>{}

public class PlayerUnitControl : NetworkBehaviour {

	public GameObject TestUnit;
	public float minFollowerDistance = 4f;
	public SyncListSquadUnit squad = new SyncListSquadUnit();

	void Awake(){
		squad.Callback = OnSquadChanged;
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();

	}
	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		CmdTestSpawnSquad();
	}

	/// <summary>
	/// Tests a squad by spawning it in
	/// </summary>
	[Command]
	void CmdTestSpawnSquad(){
		int count = 3;
		for(int i = 0; i < count; i++){
			GameObject go = GameObject.Instantiate (TestUnit, transform.position + (Vector3)(Random.insideUnitCircle * .1f), Quaternion.identity);
			NetworkServer.Spawn(go);
			SquadUnit su = SquadUnit.GameObjectToSquadUnit(go);
			//Should be assigned at authority, in this case the server
			CmdAddUnitToSquad(su);
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void CmdAddUnitToSquad(GameObject go){
		CmdAddUnitToSquad(SquadUnit.GameObjectToSquadUnit(go));
	}

	[Command]
	void CmdAddUnitToSquad(SquadUnit s){
		s.follower.target = transform;
		s.follower.minDistance = minFollowerDistance;
		//Reset speed multiplier to normal
		s.unit.GetComponent<CharacterMovement>().speedMultiplier = 1;

		squad.Add(s);
	}

	void CmdRemoveUnitFromSquad(GameObject go){
		CmdRemoveUnitFromSquad(SquadUnit.GameObjectToSquadUnit(go));
	}

	[Command]
	void CmdRemoveUnitFromSquad(SquadUnit s){
		squad.Remove(s);
	}


	void OnSquadChanged(SyncListSquadUnit.Operation op, int index){
		//SyncListStruct<SquadUnit>.Operation.


	}

	/// <summary>
	/// When Player interacts with a tile
	/// </summary>
	/// <param name="tile">Tile.</param>
	[Command]
	void CmdTileInteract(NetworkInstanceId tileID){
		Tile tile = ClientScene.FindLocalObject(tileID).GetComponent<Tile>();

		if(tile.units.Count > 0){
			//Add unit to squad, remove from tile
			CmdAddUnitToSquad(tile.units[0]);
			tile.units.Remove(tile.units[0]);
		}
		else{
			//Remove unit from squad, add to tile
			if(squad.Count > 0){
				tile.CmdAddUnit(squad[0]);
				CmdRemoveUnitFromSquad(squad[0]);
			}
		}

	}

	void LocalTileInteract(Tile tile){
		CmdTileInteract(tile.netId);
	}


	public void OnTileCollision(Tile tile){
		if(isServer){
			
		}
		if(isLocalPlayer){
			if(Input.GetButtonDown("Interact")){
				LocalTileInteract(tile);
			}
		}
		if(isClient){
			
		}
	}

}
