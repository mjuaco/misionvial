using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfazJugable : MonoBehaviour
{
    public List<GameObject> Botones = new List<GameObject>();
    public Image FundidoNegro;
    public GameObject RandomBullshit;

    public void Activar()
    {
        foreach (var trigger in Botones)
        {
            trigger.SetActive(true);
        }
    }
    public void Desactivar()
    {
        foreach (var trigger in Botones)
        {
            trigger.SetActive(false);
        }
    }
}
