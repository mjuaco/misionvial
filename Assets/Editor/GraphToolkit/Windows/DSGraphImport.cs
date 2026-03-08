using UnityEngine;
using UnityEditor.AssetImporters;
using Unity.GraphToolkit.Editor;
using System;
using System.Collections.Generic;
using System.Linq;

[ScriptedImporter(1, DSGraph.AssetExtension)]
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
                ProgressDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            }else if (iNode is ChoiceNode choiceNode)
            {
                ProgressChoiceNode(choiceNode, runtimeNode, nodeIDMap);
            }
                runtimeGraph.allNodes.Add(runtimeNode);
        }

        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);

    }

    private void ProgressDialogueNode(DialogueNode node, runtimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var nextNodePort = node.GetOutputPortByName("Output")?.firstConnectedPort;

        if (nextNodePort != null)
        {
            var nextNode = nextNodePort.GetNode();

            if (nodeIDMap.TryGetValue(nextNode, out var nextID))
            {
                runtimeNode.NextNodeID = nextID;
            }
        }
    }

    private void ProgressChoiceNode(ChoiceNode node, runtimeDialogueNode runtimeNode, Dictionary<INode, string> nodeIDMap)
    {
        runtimeNode.SpeakerName = GetPortValue<string>(node.GetInputPortByName("Speaker"));
        runtimeNode.DialogueText = GetPortValue<string>(node.GetInputPortByName("Dialogue"));

        var choiceOutputPorts = node.GetOutputPorts().Where(p => p.name.StartsWith("Choice_"));

        foreach (var outputPort in choiceOutputPorts)
        {
            var index = outputPort.name.Substring("Choice_".Length);
            var textPort = node.GetInputPortByName($"ChoiceText_{index}");

            var choiceData = new ChoiceData()
            {
                choiceText = GetPortValue<string>(textPort),
                desinationNodeID = outputPort.firstConnectedPort != null ? nodeIDMap[outputPort.firstConnectedPort.GetNode()] : null
            };
            runtimeNode.Choices.Add(choiceData);
        }
    }

    private T GetPortValue<T>(IPort port)
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
        port.TryGetValue(out T fallbackValue);
        return fallbackValue;
    }
}
