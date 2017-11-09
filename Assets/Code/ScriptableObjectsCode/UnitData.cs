using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObject/UnitData", order = 1)]
public class UnitData : ScriptableObject {

	public RuntimeAnimatorController unitAnimator;
    public SpriteSet unitSpriteSet;
    public GameObject[] weapon;

    //Unit stats
    public int health = 100;
    public float scale = 1.0f;
    public float dmgMult = 1.0f;
    public float defMult = 1.0f;
    public float attackRateMult = 1.0f;
    public float movementSpeedMult = 1.0f;

    //Multiplier for having a hat or weapon drop
    public float itemRate = 1.0f; 

}
