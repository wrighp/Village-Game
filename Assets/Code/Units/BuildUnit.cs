using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildUnit {

	// Use this for initialization
	public static void Build(GameObject unit, UnitData unitData) {
        //Add body parts from possible selection
        //Maybe add Color list possibilites (Skin color for goblins/humans, charred/bleached bones for skeletons)
        unit.transform.Find("RArm").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.rightArm.Pick();
        unit.transform.Find("LArm").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.leftArm.Pick();
        unit.transform.Find("Body").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.body.Pick();
        unit.transform.Find("Head").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.head.Pick();
        unit.transform.Find("LLeg").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.leftLeg.Pick();
        unit.transform.Find("RLeg").GetComponent<SpriteRenderer>().sprite = unitData.unitSpriteSet.rightLeg.Pick();

        //Add the animation set for the unit
        if(unitData.unitAnimator != null){
            Animator anim = unit.AddComponent<Animator>();
			anim.runtimeAnimatorController = unitData.unitAnimator;
        }

        //Socket the Weapon prefab to the Right Arm
		if(unitData.weapon.Length > 0) {
			GameObject weapon = GameObject.Instantiate(unitData.weapon.Pick());
            weapon.transform.SetParent(unit.transform.Find("RArm"));
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = Vector3.one;
        }

        /*Modify combat script stats from here
        *
        *
        *
        */
    }

}
