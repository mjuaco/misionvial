using UnityEngine;
using System;
using System.Collections.Generic;

public class runtimeDialogueGraph : ScriptableObject
{
    public string entryNodeID;
    public List<runtimeDialogueNode> allNodes = new List<runtimeDialogueNode>();
}

[Serializable]
public class runtimeDialogueNode 
{
    public string NodeID;
    public string SpeakerName;
    public string DialogueText;
    public List<ChoiceData> Choices = new List<ChoiceData>();
    public string NextNodeID;
}

[Serializable]
public class ChoiceData
{
    public string choiceText;
    public string desinationNodeID;
    public string eventID;

}