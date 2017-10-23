using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//State update channel
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class PhysicsSync : NetworkBehaviour {

	public struct Physics2DData{
		public Vector3 position;
		public Vector2 velocity;
		public Quaternion rotation;
		public float angularVelocity;

		public Physics2DData(Rigidbody2D rb){
			velocity = rb.velocity;
			position = rb.transform.position;
			rotation = rb.transform.rotation;
			angularVelocity = rb.angularVelocity;
		}

	}

	[SyncVar(hook="UpdateRigidbody")]
	Physics2DData data;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rb = rb ?? GetComponent<Rigidbody2D>();
		if(!hasAuthority){
			return;
		}

		Physics2DData p2d = new Physics2DData(rb);
		CmdSetData(p2d);

	}

	[Command (channel = 3)]
	void CmdSetData(Physics2DData p2d){
		data = p2d;
	}

	void UpdateRigidbody(Physics2DData data){
		rb = rb ?? GetComponent<Rigidbody2D>();
		rb.velocity = data.velocity;
		transform.position = data.position;
		transform.rotation = data.rotation;
		rb.angularVelocity = data.angularVelocity;
	}


}
