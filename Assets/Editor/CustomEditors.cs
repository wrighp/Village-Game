using UnityEngine;
using System.Collections;
using UnityEditor;

public class CustomEditors
{

	[CustomEditor(typeof(CustomPrefabsRegisterTool))]
	public class CustomPrefabsRegister : Editor
	{

		public override void OnInspectorGUI()
		{

			DrawDefaultInspector();

			CustomPrefabsRegisterTool myScript = (CustomPrefabsRegisterTool)target;
			EditorUtility.SetDirty(target);

			if (GUILayout.Button("Load prefabs"))
			{
				myScript.RegisterSpawnablePrefabsFromResources();
			}
		}

	}

}