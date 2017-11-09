using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBody : MonoBehaviour {
	public GameObject bodyPrefab;
	// Use this for initialization
	void Start () {
		GameObject go = (GameObject)Instantiate(bodyPrefab,transform);
		GetComponent<CharacterMovement>().SetBody(go);
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
