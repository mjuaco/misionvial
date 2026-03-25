using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfazJugable : MonoBehaviour
{
    public List<EventTrigger> Botones = new List<EventTrigger>();
    public Image FundidoNegro;
    public GameObject RandomBullshit;

    public void Activar()
    {
        foreach (var trigger in Botones)
        {
            trigger.enabled = true;
        }
    }
    public void Desactivar()
    {
        foreach (var trigger in Botones)
        {
            trigger.enabled = false;
        }
    }
}
