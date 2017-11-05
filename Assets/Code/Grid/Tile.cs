using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tile : NetworkBehaviour {

	CircleCollider2D circleCollider;

	[SyncVar]public int posX;
	[SyncVar]public int posY;
	bool triggering;

    public Building building = null;

	public SyncListSquadUnit units = new SyncListSquadUnit();

    RectTransform buildMenu;

	void Awake(){
		circleCollider = GetComponent<CircleCollider2D>();
	}

	// Use this for initialization
	void Start () {
        buildMenu = GameObject.Find("BuildMenu").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

		//Debug
		//Scales Debug circle to the size of the actual circle drawn
		float maxDimension = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
		Color c = new Color(0,0,.5f,.125f);
		if(triggering){
			c = new Color(1f,0,0 ,.125f);
		}
		PlayerDebug.DrawCircle(transform.position, circleCollider.radius * maxDimension, c);

		foreach(SquadUnit su in units){
			if(Vector2.Distance(su.unit.transform.position, transform.position) < .1f){
				su.unit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				su.unit.transform.position = transform.position;
				if(isServer){
					su.unit.GetComponent<CharacterMovement>().speedMultiplier = 0;
					su.unit.GetComponent<Rigidbody2D> ().simulated = false;
				}
			}
		}


	}

	void FixedUpdate(){
		triggering = false;
	}

	void OnTriggerEnter2D(Collider2D collider){
		OnTriggerStay2D (collider);
	}





	/// <summary>
	/// Raises the trigger event, only Player layers can interact with tile triggers
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerStay2D(Collider2D collider){
		//Call function on the player unit controller, so that it may interact with this Tile
		var playerUnitController = collider.GetComponent<PlayerUnitControl>();
		triggering = true;
		playerUnitController.OnTileCollision(this);
        if (building == null && playerUnitController.isLocalPlayer) {
            buildMenu.position = Camera.main.WorldToScreenPoint(playerUnitController.transform.position + Vector3.up);
        } else if(playerUnitController.isLocalPlayer) {
            buildMenu.position = new Vector3(-100, -100, 1);
        }
        if (Input.GetButtonDown("QuickMenu1") && building == null && playerUnitController.isLocalPlayer){
			Cmds.i.PerformBuild(0, this.gameObject);
        } else if(Input.GetButtonDown("QuickMenu2") && building == null && playerUnitController.isLocalPlayer) {
            Cmds.i.PerformBuild(1, this.gameObject);
        }
	}

	void OnTriggerExit2D(Collider2D collider){
        buildMenu.position = new Vector3(-100, -100, 1);
    }

	[Command]
	public void CmdAddUnit(SquadUnit unit){
		units.Add(unit);
		unit.follower.target = transform;
		unit.follower.minDistance = 0;
	}

	/// <summary>
	/// Gets the tiles adjacent.
	/// </summary>
	/// <returns>The tiles adjacent.</returns>
	/// <param name="orthogonal">If set to <c>true</c> will exclude diagonal tiles and only include orthogonal ones that are adjacent.</param>
	public List<Tile> GetTilesAdjacent(bool orthogonal){
		return TileManager.instance.GetTilesAdjacent(this, orthogonal);
	}

	/// <summary>
	/// Gets the adjacent tile of 'tile' in the given direction.
	/// </summary>
	/// <returns>The adjacent tile, or null if out of bounds.</returns>
	/// <param name="direction">Direction.</param>
	public Tile GetAdjacent(TileDirection direction){
		return TileManager.instance.GetAdjacent(this,direction);
	}
}
