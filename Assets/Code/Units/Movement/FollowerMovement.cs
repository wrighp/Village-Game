using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Follower will move towards target if it is more than the min distance away from it
/// </summary>
[RequireComponent (typeof (CharacterMovement))]
public class FollowerMovement : NetworkBehaviour{

	CharacterMovement movement;
	public float minDistance = 4f;
	public Transform target;
    [SyncVar]
    public bool rooted = false;

	void Awake(){
		movement = GetComponent<CharacterMovement>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rooted){
            foreach (SpriteRenderer sr in this.GetComponentsInChildren<SpriteRenderer>())
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
            return;
        } else {
            foreach(SpriteRenderer sr in this.GetComponentsInChildren<SpriteRenderer>())
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        }
		if(!hasAuthority){
			return;
		}

		//Make sure to use Raw Axis or movement becomes very lethargic
		movement.direction = target.position - transform.position;
		float dist = movement.direction.magnitude;
		if(dist > minDistance){
			movement.direction.Normalize();
		}
		else{
			movement.direction = Vector2.zero;
		}

	}
}
