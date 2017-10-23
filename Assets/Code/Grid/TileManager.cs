using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TileManager : NetworkBehaviour {

	public GameObject tilePrefab;
	public int width;
	public int height;
	public Vector2 spacing;
	public float offset;

	Tile[,] tiles;

	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();
		if(!isServer){
			return;
		}

		DebugGrid grid = FindObjectOfType<DebugGrid>();
		width = grid.xSegments;
		height = grid.ySegments;
		spacing.x = grid.width / (float)width;
		spacing.y = grid.height / (float)height;

		tiles = new Tile[width,height];

		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				GameObject go = (GameObject)GameObject.Instantiate(tilePrefab, new Vector2(i * spacing.x + offset * j,j * spacing.y),Quaternion.identity,transform);
				NetworkServer.Spawn(go);
				tiles[i,j] = go.GetComponent<Tile>();
			}	
		}

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
