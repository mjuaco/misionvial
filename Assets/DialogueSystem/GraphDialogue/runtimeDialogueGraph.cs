using UnityEngine;
using System.Collections.Generic;

public class runtimeDialogueGraph : ScriptableObject
{
    public string entryNodeID;
    public List<runtimeDialogueNode> allNodes = new List<runtimeDialogueNode>();
}

public class runtimeDialogueNode : ScriptableObject
{
    public string NodeID;
    public string SpeakerName;
    public string DialogueText;
    public string NextNodeID;
}