using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Swing Effect", menuName = "SwingEffects/WeaponSwing", order = 1)]
public class SwingEffect : ScriptableObject {

	private bool isServer;

	public bool IsServer {
		get {
			return isServer;
		}
		set {
			isServer = value;
		}
	}

	/// <summary>
	/// Called at beginning of attack.
	/// 
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	public virtual void OnSwing(AttackSystem attacker){
		
	}

	/// <summary>
	/// Called after windup period before any OnHit calls, attack is not guaranteed to hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	/// <param name="totalHit">Number of total objects that are going to be hit.</param>
	public virtual void OnBeforeHit(AttackSystem attacker, int totalHit){
		
	}

	/// <summary>
	/// Called for each character that is hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	/// <param name="hit">Character that is hit.</param>
	/// <param name="numHit">Which number hit was the object (0 to totalHit).</param>
	/// <param name="totalHit">Number of total objects that are going to be hit.</param>
	public virtual void OnHit(AttackSystem attacker, GameObject hit, int numHit, int totalHit){

	}

	/// <summary>
	/// Called if no character was hit.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	public virtual void OnMiss(AttackSystem attacker){

	}

	/// <summary>
	/// Called after all OnHit or OnMiss events, at beginning of backswing.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	public virtual void OnAfterHit(AttackSystem attacker){

	}

	/// <summary>
	/// Called after end of backswing.
	/// </summary>
	/// <param name="attacker">Attacker.</param>
	public virtual void OnComplete(AttackSystem attacker){

	}
}
