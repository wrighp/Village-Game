#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor;

public class SpawnPreprocessor : IPreprocessBuild {

	public int callbackOrder { get { return 0; } }

	public void OnPreprocessBuild(BuildTarget target, string path) {
		
		if(CustomPrefabsRegisterTool.instance.updateOnPrebuild){
			Debug.Log("Adding prefabs to spawnlist during prebuild...");
			CustomPrefabsRegisterTool.instance.RegisterSpawnablePrefabsFromResources();
		}
	}
}
#endif