using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDebug : MonoBehaviour {

	public static LineDebug instance;
	public Material mat;

	List<Vector3> verts;
	List<Color> colors;

	void Awake(){
		instance = this;
		verts = new List<Vector3>();
		colors = new List<Color>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void DrawLine(Vector3 v1, Vector3 v2, Color? color = null){
		color = color ?? Color.blue;
		instance.AddLine(v1,v2,color);
	}
	public static void DrawRay(Vector3 v1, Vector3 v2, Color? color = null){
		color = color ?? Color.blue;
		instance.AddLine(v1,v1 + v2,color);
	}


	public void AddLine(Vector3 v1, Vector3 v2, Color? color = null){
		color = color ?? Color.blue;
		verts.Add(v1);
		verts.Add(v2);
		colors.Add(color.Value);
	}

	// LateUpdate is called once per frame
	void LateUpdate(){
		for (int i = 0, j = 0, colorsCount = colors.Count; i < colorsCount; i++, j += 2) {
			Debug.DrawLine(verts[j], verts[j + 1], colors[i], 0, false);
		}
	}

	void OnPostRender () {

		GL.PushMatrix();
		mat.SetPass(0);
		mat.color = Color.white;
		//GL.LoadOrtho();
		GL.Begin(GL.LINES);
		Color c = Color.white;

		for (int i = 0, j = 0, colorsCount = colors.Count; i < colorsCount; i++, j += 2) {
			if(c != colors[i]){
				GL.Color(colors[i]);
				c = colors[i];
			}
			GL.Vertex(verts [j]);
			GL.Vertex(verts [j + 1]);
		}

		GL.End();
		GL.PopMatrix();

		verts.Clear();
		colors.Clear();
	}
}
