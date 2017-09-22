using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObject/QuestObject", order = 1)]
public class QuestObject : ScriptableObject {
	public string body = "";
}