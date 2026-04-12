using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<runtimeDialogueGraph> runtimeGraph = new List<runtimeDialogueGraph>();
    private Coroutine Coroutine;
    public int Index;
    public int Timer_Time;
    public float Timer;

    [Header("UI Components")]
    public GameObject Dialoguepanel;
    public SemaforoVida semaforo;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;
    public GameObject PanelNoticia;
    public Image Meter;

    [Header("UI Buttons Components")]
    public Button choiceButtonPrefab;
    public Transform choiceButtonConteiner;
    public Animator PopUpAnimation;

    private Dictionary<string, runtimeDialogueNode> _nodeLookup = new Dictionary<string, runtimeDialogueNode>();
    private runtimeDialogueNode _currentNode;

    public Pregunta pregunta;

    private void Start()
    {
        semaforo = GetComponent<SemaforoVida>();
    }
    public void prueba()
    {
        if (runtimeGraph == null || runtimeGraph.Count <= Index)
        {
            Debug.LogError($"Error: No hay un grafo asignado en el índice {Index} de la lista.");
            return;
        }

        _nodeLookup.Clear();

        foreach (var node in runtimeGraph[Index].allNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }

        if (!string.IsNullOrEmpty(runtimeGraph[Index].entryNodeID))
        {
            showDialogue(runtimeGraph[Index].entryNodeID);
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
            Coroutine = StartCoroutine(DurantePregunta());
            PopUpAnimation.enabled = true;
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
                        ExecuteEvent(choice.eventID);

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
    private void ExecuteEvent(string eventID)
    {
        if (string.IsNullOrEmpty(eventID)) return;

        switch (eventID)
        {
            case "Incorrecta":
                semaforo.RegistrarFallo();
                break;

            case "Correcta":
                Debug.Log("Tedi los puntos");
                break;

            default:
                Debug.Log("No seleccionaste, pérdida de puntos");
                break;
        }
    }

    private void endDialogue()
    {
        if (Coroutine != null)
        {
            StopCoroutine(Coroutine);
            Coroutine = null; 
        }

        if (Meter != null) Meter.gameObject.SetActive(false);

        _currentNode = null;
        Dialoguepanel.SetActive(false);

        if (Index < runtimeGraph.Count - 1)
        {
         Index++;
            
        }

        foreach (Transform child in choiceButtonConteiner)
        {
            Destroy(child.gameObject);
        }

        if (pregunta != null) StartCoroutine(pregunta.SalirPregunta());
    }

    private IEnumerator DurantePregunta()
    {
        float convertion = 1520 / Timer_Time;
        Timer = Timer_Time;
        Meter.rectTransform.sizeDelta = new Vector2(convertion * Timer_Time, 50);
        yield return new WaitForSeconds(1);
        Meter.gameObject.SetActive(true);
        while (Timer > 0.05f)
        {
            Meter.rectTransform.sizeDelta = new Vector2(convertion * Timer, 50);
            Timer -= Time.deltaTime;
            yield return null;

            if (Timer < Timer_Time * 0.2f)
            {
                Meter.color = Color.red;
            }
            else if (Timer < Timer_Time * 0.6f)
            {
                Meter.color = Color.yellow;
            }
            else
            {
                Meter.color = Color.green;
            }
        }
        ExecuteEvent("Nada");
        endDialogue();
    }
}
