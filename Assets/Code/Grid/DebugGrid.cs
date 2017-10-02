using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGrid : MonoBehaviour {

	public Color color = Color.white;
	public float width, height;
	public Vector3 positionOffset;
	public Material mat;
	public int xSegments, ySegments;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update(){

		float xPart = width / (xSegments);
		float yPart = height / (ySegments);
		for(int i = 0; i < xSegments + 1; i++){
			Vector3 v1 = positionOffset + new Vector3(xPart * (i), 0);
			Vector2 v2 = positionOffset + new Vector3(xPart * (i + 1), height);
			PlayerDebug.DrawLine(v1, v2, color);
		}
		for(int j = 0; j < ySegments + 1; j++){
			Vector3 v1 = positionOffset + new Vector3(xPart * j / ySegments, yPart * j);
			Vector3 v2 = positionOffset + new Vector3(width + xPart * j / ySegments, yPart * (j));
			PlayerDebug.DrawLine(v1, v2, color);
		}
	}
}
