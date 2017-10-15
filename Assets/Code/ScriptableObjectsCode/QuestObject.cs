using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using System;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObject/QuestObject", order = 1)]
public class QuestObject : ScriptableObject
{
    public string QuestName = "";
    public Decision Decision;
}

//Description of event and all the choices, each choice can go to a branch
[Serializable]
public class Decision
{
    public string decisionDescription = "";
    public Choice[] options;
    public Decision[] branches;
}


[Serializable]
public class Choice
{
    public string choiceDescription = "";
    public DecisionOption option;
}