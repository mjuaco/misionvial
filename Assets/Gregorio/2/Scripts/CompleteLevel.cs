using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public List<GameObject> Botones = new List<GameObject>();
    public CámaraSeguimiento Cámara;
    void Start()
    {
        foreach (var gameObject in Botones)
        {
            gameObject.SetActive(false);
        }
        Cámara.enabled = false;
    }
}
