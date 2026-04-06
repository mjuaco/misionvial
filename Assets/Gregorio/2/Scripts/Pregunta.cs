using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Pregunta : MonoBehaviour
{
    public InterfazJugable InterfazJugable;
    public ManejoCarro Carro;
    public Rigidbody CarroFísicas;
    public float Posición;
    public float Velocidad;
    private float Tiempo;
    private float Temporizador;
    public GameObject dialogueManager;
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
       dialogueManager.GetComponent<DialogueManager>().enabled = true;
    }

    public IEnumerator SalirPregunta()
    {
        dialogueManager.GetComponent<DialogueManager>().enabled = false;
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
