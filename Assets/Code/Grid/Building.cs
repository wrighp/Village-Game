using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Building : MonoBehaviour{
    protected SupplyData sD;

    static public bool CanBuild(){
        return true;
    }

    abstract public void OnBuild();

    abstract public void OnRemove();

    abstract public void OnInteract();

    abstract public void OnTurnStart();

    abstract public void OnTurnEnd();

}
