using System.Collections.Generic;
using UnityEngine;

public class GeneradorNiveles : MonoBehaviour
{
    public List<GameObject> Orden = new List<GameObject>();
    public List<GameObject> Carretera = new List<GameObject>();
    public GameObject Carro;
    public int Contador = 0; 

    void Start()
    {
        Nuevo();
    }

    public void Antiguo()
    {
        for (int i = 0; i < Orden.Count; i++)
        {
            GameObject instancia;

            if (i % 2 == 0)
            {
                instancia = Instantiate(Orden[i], Vector3.zero, Quaternion.identity);
                Carretera.Add(instancia);
                Carretera[i].transform.Find("Collider").gameObject.SetActive(false);
            }
            else
            {
                Vector3 posicion = Orden[i - 1].transform.Find("Final").position;
                instancia = Instantiate(Orden[i], posicion, Quaternion.identity);
                Carretera.Add(instancia);
                Carretera[i].transform.Find("Collider").gameObject.SetActive(true);
            }

            instancia.SetActive(false);
        }

        Carretera[Contador].gameObject.SetActive(true); Carretera[Contador + 1].gameObject.SetActive(true);
    }

    public void Nuevo()
    {
        for (int i = 0; i < Orden.Count; i++)
        {
            GameObject instancia;

            if (i == 0)
            {
                instancia = Instantiate(Orden[i], Vector3.zero, Quaternion.identity);
                Carretera.Add(instancia);
            }
            else
            {
                instancia = Instantiate(Orden[i], Carretera[i - 1].transform.Find("Final").position, Quaternion.identity);
                Carretera.Add(instancia);
            }
        }
    }

    public void Actualización()
    {
        Contador += 2;
        Carretera[Contador - 2].gameObject.SetActive(false);
        Carretera[Contador].gameObject.SetActive(true);
        Carro.transform.position = Carretera[Contador].transform.Find("Inicio").position;
        Carretera[Contador - 1].gameObject.SetActive(false);
        Carretera[Contador + 1].gameObject.SetActive(true);
    }
}
