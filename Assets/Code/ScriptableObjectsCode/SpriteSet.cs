using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpriteSet", menuName = "ScriptableObject/SpriteSet", order = 1)]
public class SpriteSet : ScriptableObject {
    //Unit Body parts
    public Sprite[] head, body, leftArm, rightArm, leftLeg, rightLeg;
}
