using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponSwing : NetworkBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// Called at beginning of attack.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	protected virtual void OnSwing(Attacking attacker){
		
	}

	/// <summary>
	/// Called after windup period before any OnHit calls, attack is not guaranteed to hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	protected virtual void OnBeforeHit(Attacking attacker){
		
	}

	/// <summary>
	/// Called for each character that is hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	/// <param name="hit">Character that is hit.</param>
	/// <param name="numHit">Number of other total characters that were or will be hit in this same attack.</param>
	protected virtual void OnHit(Attacking attacker, GameObject hit, int numHit){

	}

	/// <summary>
	/// Called if no character was hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	protected virtual void OnMiss(Attacking attacker){

	}

	/// <summary>
	/// Called after all OnHit or OnMiss events, at beginning of backswing.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	protected virtual void OnAfterHit(Attacking attacker){

	}

	/// <summary>
	/// Called after end of backswing.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	protected virtual void OnComplete(Attacking attacker){

	}
}
