using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> Botones = new List<GameObject>();
    public List<GameObject> Pages = new List<GameObject>();
    public int Index;

    public void Next()
    {
        Index++;
        Pages[Index].SetActive(true);
        Pages[Index - 1].SetActive(false);
    }

    public void Previous()
    {
        Index--;
        Pages[Index].SetActive(true);
        Pages[Index + 1].SetActive(false);
    }

    public void FinishTutorial()
    {
        foreach (var gameObject in Botones)
        {
            gameObject.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
