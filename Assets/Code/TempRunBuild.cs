using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRunBuild : MonoBehaviour {

    public UnitData uD;

	//This is a temoprary class for building sprites, the build should be called from the units main script
	void Awake () {
        BuildUnit.Build(this.gameObject, uD);
	}
}
