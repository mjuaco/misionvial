using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pregunta : MonoBehaviour
{
    public InterfazJugable InterfazJugable;
    public ManejoCarro Carro;
    public Rigidbody CarroFísicas;
    public float Posición;
    public float Velocidad;
    public float Tiempo;
    public float Temporizador;

    private void Start()
    {
        InterfazJugable = GameObject.Find("Canvas").GetComponent<InterfazJugable>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carro"))
        {
            Carro = other.transform.root.GetComponent<ManejoCarro>();
            CarroFísicas = other.transform.root.GetComponent<Rigidbody>();
            Velocidad = CarroFísicas.linearVelocity.x;
            Posición = transform.parent.position.x - Carro.transform.position.x;
            Tiempo = Posición / Velocidad;
            InterfazJugable.Desactivar();
            StartCoroutine(EntrarPregunta());
        }
    }

    public IEnumerator EntrarPregunta()
    {
        Carro.EnEncuesta = true;
        Temporizador = 0.0f;
        while (Temporizador < Tiempo)
        {
            CarroFísicas.linearVelocity = new Vector3(Mathf.Lerp(Velocidad, 0f, Temporizador / Tiempo), 0f);
            Temporizador += Time.deltaTime;
            yield return null;
        }
        Carro.Presionado = 2;
        StartCoroutine(DurantePregunta());
    }

    public IEnumerator DurantePregunta()
    {
        Temporizador = 0.0f;
        InterfazJugable.FundidoNegro.gameObject.SetActive(true);
        while (Temporizador < 0.5f)
        {
            InterfazJugable.FundidoNegro.color = Color.Lerp(new Color(0f, 0f, 0f, 0f), new Color(0f, 0f, 0f, 0.65f), Temporizador / 0.5f);
            Temporizador += Time.deltaTime;
            yield return null;
        }
        InterfazJugable.RandomBullshit.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        InterfazJugable.RandomBullshit.gameObject.SetActive(false);
        Temporizador = 0.0f;
        while (Temporizador < 0.5f)
        {
            InterfazJugable.FundidoNegro.color = Color.Lerp(new Color(0f, 0f, 0f, 0.65f), new Color(0f, 0f, 0f, 0f), Temporizador / 0.5f);
            Temporizador += Time.deltaTime;
            yield return null;
        }
        InterfazJugable.FundidoNegro.gameObject.SetActive(false);
        StartCoroutine(SalirPregunta());
    }

    public IEnumerator SalirPregunta()
    {
        Carro.Cambio = false;
        Carro.Presionado = 0;
        Temporizador = 0.0f;
        while (Temporizador < (Tiempo/2))
        {
            CarroFísicas.linearVelocity = new Vector3(Mathf.Lerp(0f, Velocidad, Temporizador / (Tiempo/2)), 0f);
            Temporizador += Time.deltaTime;
            yield return null;
        }
        InterfazJugable.Activar();
        Carro.EnEncuesta = false;
        this.gameObject.SetActive(false);
    }
}
