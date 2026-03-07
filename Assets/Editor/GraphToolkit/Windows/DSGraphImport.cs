using UnityEngine;
using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

[ScriptedImporter(1, DSGraph.AssetExtensions)]
public class DSGraphImport : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DSGraph editorGraph = GraphDatabase.LoadGraphForImporter<DSGraph>(ctx.assetPath);
        runtimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<runtimeDialogueGraph>();

        var nodeIDMap = new Dictionary<INode, string>();
        foreach (var node in editorGraph.GetNodes())
        {
            nodeIDMap[node] = Guid.NewGuid().ToString();
        }

        var startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if (startNode != null)
        {
            var entryPort = startNode.GetOutputPorts().FirstOrDefault()?.firstConnectedPort;
            if (entryPort != null)
            {
                runtimeGraph.entryNodeID = nodeIDMap[entryPort.GetNode()];
            }
        }

        foreach (var iNode in editorGraph.GetNodes())
        {
            if (iNode is StartNode || iNode is EndNode) continue;
            var runtimeNode = new runtimeDialogueNode { NodeID = nodeIDMap[iNode] };
            if (iNode is DialogueNode dialogueNode)
            {
                progressDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            }
        }
        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);

    }

    private void progressDialogueNode(DialogueNode node, runtimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortVaule<string>(node.GetOutputPortByName("Speaker"));
        runtimeNode.SpeakerName = GetPortVaule<string>(node.GetOutputPortByName("Dialogue"));

        var nextNodePort = node.GetOutputPortByName("Output")?.firstConnectedPort;
        if (nextNodePort != null) 
            runtimeNode.NextNodeID = nodeIDMap[nextNodePort.GetNode()];
    }

    private T GetPortVaule<T>(IPort port)
    {
        if (port == null) return default;
        if (port.isConnected)
        {
            if (port.firstConnectedPort.GetNode() is IVariableNode variableNode)
            {
                variableNode.variable.TryGetDefaultValue(out T value);
                return value;
            }
        }
        port.TryGetValue(out T fallbackVaule);
        return fallbackVaule;
    }
}
