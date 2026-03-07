using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
  public runtimeDialogueGraph runtimeGraph;

    
    public GameObject Dialoguepanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    private Dictionary<string, runtimeDialogueNode> _nodeLookup = new Dictionary<string, runtimeDialogueNode>();
    private runtimeDialogueNode _runtimeNode;

    private void Start()
    {
     foreach (var node in runtimeGraph.allNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }
        
    }
}
