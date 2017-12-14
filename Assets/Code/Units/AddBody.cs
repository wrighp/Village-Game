using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AddBody : NetworkBehaviour {
	public GameObject bodyPrefab;
	// Use this for initialization
	public void BuildBody (UnitData unitData) {
        GameObject go = (GameObject)Instantiate(bodyPrefab,transform);
		GetComponent<CharacterMovement>().SetBody(go);
        BuildUnit.Build(go, unitData);
        Destroy(this);
	}
}
