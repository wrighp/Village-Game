using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGrid : MonoBehaviour {

	public Color color = Color.white;
	public float width, height;
	public Vector3 positionOffset;
	public Material mat;
	public int xSegments, ySegments;
	List<Vector2> verts;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update(){
		//Build List of lines to use on OnPostRender
		verts = new List<Vector2>(xSegments + ySegments + 4);

		float xPart = width / (xSegments);
		float yPart = height / (ySegments);
		for(int i = 0; i < xSegments + 1; i++){
			Vector3 v1 = positionOffset + new Vector3(xPart * (i), 0);
			Vector2 v2 = positionOffset + new Vector3(xPart * (i + 1), height);
			verts.Add(v1);
			verts.Add(v2);
			Debug.DrawLine(v1, v2, color, 0,true);
		}
		for(int j = 0; j < ySegments + 1; j++){
			Vector3 v1 = positionOffset + new Vector3(xPart * j / ySegments, yPart * j);
			Vector3 v2 = positionOffset + new Vector3(width + xPart * j / ySegments, yPart * (j));
			verts.Add(v1);
			verts.Add(v2);
			Debug.DrawLine(v1, v2,color, 0,true);
		}
	}

	void OnPostRender () {

		GL.PushMatrix();
		mat.SetPass(0);
		mat.color = color;
		//GL.LoadOrtho();
		GL.Begin(GL.LINES);
		GL.Color(color);

		for (int i = 0, vertsCount = verts.Count; i < vertsCount; i++) {
			GL.Vertex(verts [i]);
		}

		GL.End();
		GL.PopMatrix();
		verts.Clear();
	}
}
