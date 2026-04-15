using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    public Pregunta Pregunta;
    public ManejoCarro ManejoCarro;
    public InterfazJugable InterfazJugable;
    public DialogueManager dialogueManager;
    public GameObject Carruaje;
    public GameObject[] Noticias;
    public GameObject Carro;
    public SpriteRenderer Vaca;
    public CámaraSeguimiento CámaraSeguimiento;

    private void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();
    }
    //Pregunta 1
    public IEnumerator CorrectaPendiente()
    {
        Pregunta.Velocidad = 15;
        ManejoCarro.Velocidad = 15;
        yield return new WaitForSeconds(8);
        InterfazJugable.Activar();
    }
    public IEnumerator IncorrectaPendiente1()
    {
        Pregunta.Velocidad = 15;
        ManejoCarro.Velocidad = 15;
        yield return new WaitForSeconds(6f);
        float Tiempo = 0;
        while (Tiempo < 0.7f)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(15, 35, Tiempo / 0.7f);
            Tiempo += Time.deltaTime;
            yield return null;
        }
        Tiempo = 0;
        while (Tiempo < 2f)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(40, 2, Tiempo / 2f);
            if (Tiempo < 0.7f)
            {
                Carruaje.transform.localPosition = new Vector3(Mathf.Lerp(5.95f, -1.97f, Tiempo / 0.7f),
                                                               Carruaje.transform.localPosition.y,
                                                               Carruaje.transform.localPosition.z);
            }
            else if (Tiempo < 1.3f)
            {
                Carruaje.transform.localPosition = new Vector3(Mathf.Lerp(-1.97f, 5.95f, (Tiempo - 0.7f) / 0.6f),
                                                               Carruaje.transform.localPosition.y,
                                                               Carruaje.transform.localPosition.z);
            }
            else
            {
                Carruaje.transform.localPosition = new Vector3(Mathf.Lerp(5.95f, -1.97f, (Tiempo - 1.3f) / 0.7f),
                                                               Carruaje.transform.localPosition.y,
                                                               Carruaje.transform.localPosition.z);
            }
            Tiempo += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        Noticias[0].SetActive(true);
        ManejoCarro.Velocidad = 0;
    }
    public IEnumerator IncorrectaPendiente2()
    {
        Pregunta.Velocidad = 15;
        ManejoCarro.Velocidad = 15;
        yield return new WaitForSeconds(6.35f);
        float Tiempo = 0;
        Carro.transform.position = new Vector3(521.7f, 5.1f, -3.53f);
        while (Tiempo < 1.7f)
        {
            Carro.transform.position += new Vector3(-15 * Time.deltaTime, 0f, 0f);
            ManejoCarro.Velocidad = Mathf.Lerp(15, 40, Tiempo / 1.4f);
            if (Tiempo > 1)
            {
                Carruaje.transform.localPosition = new Vector3(Mathf.Lerp(5.95f, -1.97f, (Tiempo - 1) / 0.6f),
                                                               Carruaje.transform.localPosition.y,
                                                               Carruaje.transform.localPosition.z);
            }
            Tiempo += Time.deltaTime;
            yield return null;
        }
        Noticias[1].SetActive(true);
        ManejoCarro.Velocidad = 0;
    }

    //Pregunta 2
    public IEnumerator CorrectaEmerger()
    {
        Pregunta.Velocidad = 20;
        ManejoCarro.Velocidad = 20;
        Carro.transform.position = new Vector3(521.7f, 2.38f, 0f);
        Carro.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        float Tiempo = 0;
        while (Tiempo < 2f)
        {
            Carro.transform.position = new Vector3(Mathf.Lerp(578.8f, 683.5f, Tiempo / 2f), 2.38f, 0f);
            Tiempo += Time.deltaTime;
            yield return null;
        }
        Carro.transform.position = new Vector3(521.7f, -20.38f, 0f);
        Tiempo = 0;
        while (Tiempo < 0.8f)
        {
            Carruaje.transform.localPosition = new Vector3(Mathf.Lerp(5.95f, 1.06f, Tiempo / 0.8f),
                                                               Carruaje.transform.localPosition.y,
                                                               Carruaje.transform.localPosition.z);
            Tiempo += Time.deltaTime;
            yield return null;
        }
        InterfazJugable.Activar();
    }
    public IEnumerator IncorrectaEmerger1()
    {
        Pregunta.Velocidad = 15;
        float tiempo = 0;
        float duracionAceleracion = 2.0f;

        while (tiempo < duracionAceleracion)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(15, 35, tiempo / duracionAceleracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        tiempo = 0;
        float duracionManiobra = 0.8f;
        while (tiempo < duracionManiobra)
        {
            Carruaje.transform.localPosition = new Vector3(
                Mathf.Lerp(5.95f, 1.5f, tiempo / duracionManiobra),
                Carruaje.transform.localPosition.y,
                Carruaje.transform.localPosition.z
            );
            tiempo += Time.deltaTime;
            yield return null;
        }

        tiempo = 0;
        float duracionFrenado = 0.5f;
        Vector3 posicionAntesFreno = Carro.transform.position;

        while (tiempo < duracionFrenado)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(35, 0, tiempo / duracionFrenado);

            Carro.transform.position = posicionAntesFreno + new Vector3(Random.Range(-0.05f, 0.05f), 0, 0);

            tiempo += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        
        if (Noticias.Length > 2)
        {
            Noticias[2].SetActive(true);
        }

        ManejoCarro.Velocidad = 0;
    }
    public IEnumerator IncorrectaEmerger2()
    {
        Pregunta.Velocidad = 20;
        ManejoCarro.Velocidad = 20;
        float tiempo = 0;

        yield return new WaitForSeconds(1.5f); 

        while (tiempo < 1.2f)
        {
            float desplazamiento = Mathf.PingPong(tiempo * 2, 0.5f); 
            Carro.transform.localPosition += new Vector3(0, 0, desplazamiento * Time.deltaTime);

            ManejoCarro.Velocidad = Mathf.Lerp(20, 5, tiempo / 1.2f);

            tiempo += Time.deltaTime;
            yield return null;
        }

        tiempo = 0;
        while (tiempo < 0.5f)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(5, 0, tiempo / 0.5f);
            tiempo += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (Noticias.Length > 3)
        {
            Noticias[3].SetActive(true);
        }

        ManejoCarro.Velocidad = 0;
    }

    //Pregunta 3
    public IEnumerator CorrectaAnimales()
    {
        Pregunta.Velocidad = 20;
        ManejoCarro.Velocidad = 20;
        yield return new WaitForSeconds(2.5f);
        float Tiempo = 0;
        Vaca.gameObject.SetActive(true);
        while (Tiempo < 5f)
        {
            if (Tiempo < 1f)
            {
                Vaca.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), Tiempo / 1f);
                ManejoCarro.Velocidad = Mathf.Lerp(20, 0, Tiempo / 1f);
            }
            else if (Tiempo < 4f)
            {
                Vaca.transform.position = new Vector3(916.18f, Mathf.Lerp(5.2f, -2.84f, (Tiempo - 1) / 3f), 0f);
            }
            else
            {
                Vaca.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), (Tiempo - 4) / 1f);
            }
            Tiempo += Time.deltaTime;
            yield return null;
        }
        Vaca.gameObject.SetActive(false);
        dialogueManager.Finish();
        Tiempo = 0;
        while (Tiempo < 2f)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(0, 25, Tiempo / 2f);
            Tiempo += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator IncorrectaAnimales1()
    {
        Pregunta.Velocidad = 30; 
        ManejoCarro.Velocidad = 30;
        float tiempo = 0;

        Vaca.gameObject.SetActive(true);
        Vaca.color = new Color(1, 1, 1, 0);

        while (tiempo < 1.5f)
        {
            Vaca.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tiempo / 1.5f);

            Vaca.transform.position = new Vector3(916.18f, Mathf.Lerp(5.2f, 1.5f, tiempo / 1.5f), 0f);

            tiempo += Time.deltaTime;
            yield return null;
        }

        tiempo = 0;
        float duracionSusto = 0.8f;
        while (tiempo < duracionSusto)
        {
            ManejoCarro.Velocidad = Mathf.Lerp(30, 0, tiempo / duracionSusto);

            float temblor = Mathf.Sin(Time.time * 50) * 0.1f;
            Carro.transform.position += new Vector3(0, 0, temblor);

            tiempo += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (Noticias.Length > 4)
        {
            Noticias[4].SetActive(true);
        }

        ManejoCarro.Velocidad = 0;
        Vaca.gameObject.SetActive(false); 
    }
    public IEnumerator IncorrectaAnimales2()
    {
        Pregunta.Velocidad = 25;
        ManejoCarro.Velocidad = 25;
        float tiempo = 0;

        Vaca.gameObject.SetActive(true);
        Vaca.transform.position = new Vector3(916.18f, 10f, 0f);
        Vaca.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.5f); 

        while (tiempo < 1.2f)
        {
            Vaca.transform.position = new Vector3(916.18f, Mathf.Lerp(10f, -10f, tiempo / 1.2f), 0f);

            if (tiempo > 0.4f)
            {
                ManejoCarro.Velocidad = Mathf.Lerp(25, 0, (tiempo - 0.4f) / 0.4f);

                float rotacionZ = Mathf.Sin(tiempo * 40) * 5f;
                Carro.transform.rotation = Quaternion.Euler(0, 180, rotacionZ);
            }

            tiempo += Time.deltaTime;
            yield return null;
        }

        Carro.transform.rotation = Quaternion.Euler(0, 180, 0);
        ManejoCarro.Velocidad = 0;

        yield return new WaitForSeconds(2.0f);

        if (Noticias.Length > 5)
        {
            Noticias[5].SetActive(true);
        }

        Vaca.gameObject.SetActive(false);
    }
}
