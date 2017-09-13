#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

[System.Serializable]
[RequireComponent (typeof (NetworkManagerPlus))]
[ExecuteInEditMode]
public class CustomPrefabsRegisterTool : MonoBehaviour{
	//https://forum.unity.com/threads/how-to-sync-registered-spawnable-prefabs-network-hashes-between-the-client-and-server.470988/
	[Header("Load from Path Setup")]
	public string[] LoadPaths;
	public bool updateOnPrebuild = true;

	public static CustomPrefabsRegisterTool instance;

	void OnEnable(){
		instance = this;
	}
	public void RegisterSpawnablePrefabsFromResources()
	{

		NetworkManagerPlus net = GetComponent<NetworkManagerPlus>();
		List<GameObject> objects = new List<GameObject>();
		foreach (string str in LoadPaths)
		{
			GameObject[] folderObjects = Resources.LoadAll<GameObject>(str);
			objects.AddRange(folderObjects);
		}

		net.RegisterPrefabs(objects);

	}

}
#endif