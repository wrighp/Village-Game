using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using System;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObject/WeaponObject", order = 1)]
public class WeaponObject : ScriptableObject {

	public GameObject weaponPrefab;

	public WeaponAttack[] chainAttack; //Attacks that chain into each other
	public VariableSound[] swingSounds;
	public VariableSound[] hitSounds;

}

[Serializable]
public class WeaponAttack{
	//Combined windup and backswing time make up total attack time
	[Tooltip ("Time before attack hits")]
	public float windupTime;
	[Tooltip ("Max number of characters that can be hit in this strike")]
	public int cleave = 10; //Max number of characters that can be hit
	[Tooltip ("Time after weapon strike and delay before another action can be made")]
	public float backswingTime;
	[Tooltip ("Base damage applied to each character that is hit")]
	public float baseDamage;
	[Tooltip ("Functions called on weapon swing, before hit, hit, miss, after hit and completion")]
	public WeaponSwing[] swingEffects;
}

[Serializable]
public class VariableSound{
	public AudioClip sound;
	[Tooltip ("How much pitch is shifted at the start")]
	public float pitchShift = 0;
	[Tooltip ("How diffent can pitch be each call")]
	public float pitchVariance = 0; 
	public float volume = 1f;
}