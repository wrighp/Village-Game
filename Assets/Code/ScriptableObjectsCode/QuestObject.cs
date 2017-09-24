using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using System;
using UnityEngine.Networking;

public enum Choices { wood, stone, food, gold };
public enum BranchStyle { Random, Requirement };

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObject/QuestObject", order = 1)]
public class QuestObject : ScriptableObject {
    public string QuestName= "";
	public string Description = "";
    public Decision[] questDecisions;
}

[Serializable]
public class Decision{
    public string decisionDescription = "";
    public Branch[] branches;
    public BranchStyle branchStyle;
    public Requirement[] requirements;
}

[Serializable]
public class Requirement{
    public Choices type;
    public int amount;
}

[Serializable]
public class Branch{
    public string Description = "";
    public Result[] results;
}

[Serializable]
public class Result{
    public Choices value;
    public int quantity;
}