using System.Collections.Generic;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public runtimeDialogueGraph runtimeGraph;

    [Header("UI Components")]
    public GameObject Dialoguepanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("UI Buttons Components")]
    public Button choiceButtonPrefab;
    public Transform choiceButtonConteiner;

    private Dictionary<string, runtimeDialogueNode> _nodeLookup = new Dictionary<string, runtimeDialogueNode>();
    private runtimeDialogueNode _currentNode;

    public Pregunta pregunta;
    

    public void Start()
    {
        foreach (var node in runtimeGraph.allNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }
        if (!string.IsNullOrEmpty(runtimeGraph.entryNodeID))
        {
            showDialogue(runtimeGraph.entryNodeID);
        }
        else
        {
            endDialogue();
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null && _currentNode.Choices.Count == 0)
        {
            if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                showDialogue(_currentNode.NextNodeID);
            }
            else
            {
                endDialogue();
            }
        }
    }

    private void showDialogue(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            endDialogue();
            return;
        }
        _currentNode = _nodeLookup[nodeID];

        Dialoguepanel.SetActive(true);
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);

        foreach (Transform child in choiceButtonConteiner)
        {
            Destroy(child.gameObject);
        }

        if (_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
            {
                Button button = Instantiate(choiceButtonPrefab, choiceButtonConteiner);
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choice.choiceText;
                }
                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.desinationNodeID))
                        {
                            showDialogue(choice.desinationNodeID);
                        }
                        else
                        {
                            endDialogue();
                        }
                    });

                }
            }
        }
    }

    private void endDialogue()
    {
        _currentNode = null;
        Dialoguepanel.SetActive(false);

        foreach (Transform child in choiceButtonConteiner)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(pregunta.SalirPregunta());
    }
}
