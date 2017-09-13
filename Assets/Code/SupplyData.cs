using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupplyData : NetworkBehaviour {

	//Consider making resources just a Dictionary of {string,int} to account for other/hidden resources
	[SyncVar(hook = "OnWoodChange")]
	public int wood;
	[SyncVar(hook = "OnStoneChange")]
	public int stone;
	[SyncVar(hook = "OnFoodChange")]
	public int food;
	[SyncVar(hook = "OnGoldChange")]
	public int gold;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnWoodChange(int woodNew){
		wood = woodNew;
	}
	void OnFoodChange(int foodNew){
		food = foodNew;
	}
	void OnStoneChange(int stoneNew){
		stone = stoneNew;
	}
	void OnGoldChange(int goldNew){
		gold = goldNew;
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
	}

	//Reapply hooks here for initialization, as all syncvars are guaranteed to be applied before OnStartClient is called
	public override void OnStartClient ()
	{
		base.OnStartClient ();

	}
}
