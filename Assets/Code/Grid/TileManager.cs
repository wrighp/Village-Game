using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public enum TileDirection{
	North,
	NorthEast,
	East,
	SouthEast,
	South,
	SouthWest,
	West,
	NorthWest
}

public class TileManager : NetworkBehaviour {

	public static TileManager instance;

	public GameObject tilePrefab;
	public bool takeFromDebugGrid = true;
	public int width;
	public int height;
	public Vector2 spacing;
	public float offset;

	SyncListUInt tileIDs = new SyncListUInt();

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();

		if(!isServer){
			return;
		}
		tileIDs.Clear();
		if(takeFromDebugGrid){
			DebugGrid grid = FindObjectOfType<DebugGrid>();
			width = grid.xSegments;
			height = grid.ySegments;
			spacing.x = grid.width / (float)width;
			spacing.y = grid.height / (float)height;
		}



		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				GameObject go = (GameObject)GameObject.Instantiate(tilePrefab, new Vector2(i * spacing.x + offset * j,j * spacing.y),Quaternion.identity,transform);
				NetworkServer.Spawn(go);
				Tile tile = go.GetComponent<Tile>();
				tile.posX = i;
				tile.posY = j;
				tileIDs.Add(go.GetComponent<NetworkIdentity>().netId.Value);
			}	
		}

	}

	public Tile GetTile(int x, int y){
		uint id = tileIDs[x * height + y];
		GameObject go = ClientScene.FindLocalObject(new NetworkInstanceId(id));
		return go.GetComponent<Tile>();
	}

	/// <summary>
	/// Gets the tiles adjacent to 'tile'.
	/// </summary>
	/// <returns>The tiles adjacent.</returns>
	/// <param name="tile">Tile.</param>
	/// <param name="orthogonal">If set to <c>true</c> will exclude diagonal tiles and only include orthogonal ones that are adjacent.</param>
	public List<Tile> GetTilesAdjacent(Tile tile, bool orthogonal){
		List<Tile> adjacents = new List<Tile>();
		int x = tile.posX;
		int y = tile.posY;

		//Get diagonals as well
		if(!orthogonal){
			for (int i = (x > 0 ?  x - 1 : x); i <= (x + 1 < width ? x + 1 : x); i++){
				for (int j = (y > 0 ? y - 1 : y); j <= (y + 1 < height ? y + 1 : y); j++){
					if (i != x || j != y){
						adjacents.Add(GetTile(i,j));
					}
				}
			}
		}
		else{
			if(x + 1 < width){
				adjacents.Add(GetTile(x + 1,y));
			}
			if(y + 1 < height){
				adjacents.Add(GetTile(x,y + 1));
			}
			if(x > 0){
				adjacents.Add(GetTile(x - 1,y));
			}
			if(y > 0){
				adjacents.Add(GetTile(x,y - 1));
			}
		}

		return adjacents;
	}

	/// <summary>
	/// Gets the adjacent tile of 'tile' in the given direction.
	/// </summary>
	/// <returns>The adjacent tile, or null if out of bounds.</returns>
	/// <param name="tile">Tile.</param>
	/// <param name="direction">Direction.</param>
	public Tile GetAdjacent(Tile tile, TileDirection direction){

		int dx = 0;
		int dy = 0;

		switch(direction){
		case TileDirection.North:
			dx = 0;
			dy = 1;
			break;
		case TileDirection.NorthEast:
			dx = 1;
			dy = 1;
			break;
		case TileDirection.East:
			dx = 1;
			dy = 0;
			break;
		case TileDirection.SouthEast:
			dx = 1;
			dy = -1;
			break;
		case TileDirection.South:
			dx = 0;
			dy = -1;
			break;
		case TileDirection.SouthWest:
			dx = -1;
			dy = -1;
			break;
		case TileDirection.West:
			dx = -1;
			dy = 0;
			break;
		case TileDirection.NorthWest:
			dx = -1;
			dy = 1;
			break;

		}

		int i = tile.posX + dx;
		int j = tile.posY + dy;

		if(i > 0 && i < width && j > 0 && j < height){
			return GetTile(i,j);
		}
		return null;

	}

    [ClientRpc]
    public void RpcTurnEnd(){
        print("Running End");
        foreach (Building b in GameObject.FindObjectsOfType<Building>()) {
            b.OnTurnEnd();
        }
        //Temp, for testing;
        RpcTurnStart();
    }

    [ClientRpc]
    public void RpcTurnStart() {
        print("Running Start");
        foreach (Building b in GameObject.FindObjectsOfType<Building>()) {
            b.OnTurnStart();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
