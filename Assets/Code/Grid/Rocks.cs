using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : Building {

    void Start(){
        sD = GameObject.FindObjectOfType<SupplyData>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Environment/RockCluster");
    }

    public override void OnBuild() {}

    public override void OnInteract() {}

    public override void OnRemove() {}

    public override void OnTurnEnd() { print("OnTurnEnd"); }

    public override void OnTurnStart()
    {
        print("OnTurnStartRock");
        Tile tile = gameObject.GetComponent<Tile>();
        if (tile.units.Count != 0){
            //Floating Text here for stone increase on clients
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (isServer) {
                sD.stone += 10;
            }
            Destroy(this);
        }
    }
}
