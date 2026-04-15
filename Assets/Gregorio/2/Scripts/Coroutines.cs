using System.Collections;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    public Pregunta Pregunta;
    public ManejoCarro ManejoCarro;
    public InterfazJugable InterfazJugable;
    public DialogueManager DialogueManager;
    public GameObject Carruaje;
    public GameObject Noticias;
    public GameObject Carro;
    public SpriteRenderer Vaca;
    public CámaraSeguimiento CámaraSeguimiento;

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
        Noticias.SetActive(true);
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
        Noticias.SetActive(true);
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
        yield return null;
    }
    public IEnumerator IncorrectaEmerger2()
    {
        yield return null;
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
        DialogueManager.Finish();
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
        yield return null;
    }
    public IEnumerator IncorrectaAnimales2()
    {
        yield return null;
    }
}
