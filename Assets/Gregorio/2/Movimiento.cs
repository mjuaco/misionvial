using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento : MonoBehaviour
{
    public float Aceleración;
    public float Rapidez;
    public float Velocidad;
    public int Velocidad_Máxima;

    public float Transición_Aceleración;
    public bool Presionado_Acelerar;

    //public float Transición_Desaceleración;
    //public bool Presionado_Desacelerar;

    public float Tiempo_Presionado;
    public Rigidbody body;


    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Transición_Aceleración = Mathf.Clamp(Tiempo_Presionado, 0f, 1f);
        //Transición_Desaceleración = Mathf.Clamp(Tiempo_Presionado, 0f, 1f);

        Volver();

        Acelerar();
        //Desacelerar();

        if (Presionado_Acelerar)
        {
            Tiempo();
        }
        else
        {
            Restar_Tiempo();
        }

        if (Rapidez < 0)
        {
            Rapidez = 0;
        }
        else if (Rapidez > Velocidad_Máxima)
        {
            Rapidez = Velocidad_Máxima;
        }
        if (Tiempo_Presionado > 1)
        {
            Tiempo_Presionado = 1;
        }
    }

    void FixedUpdate()
    {
        Velocidad = Mathf.Clamp(Rapidez, 0f, Velocidad_Máxima);
        body.linearVelocity = transform.TransformDirection(new Vector3(0f, 0f, Velocidad));
    }

    public void Volver()
    {
        if (transform.position.z > 105)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -125);
        }
    }

    public void Presionar1()
    {
        Presionado_Acelerar = true;
    }
    //public void Presionar2()
    //{
    //    Presionado_Desacelerar = true;
    //}

    public void DejarPresionar()
    {
        Presionado_Acelerar = false;
        //Presionado_Desacelerar = false;
    }

    public void Tiempo()
    {
        Tiempo_Presionado += Time.deltaTime;
    }

    public void Restar_Tiempo()
    {
        if (Tiempo_Presionado > 0)
        {
            Tiempo_Presionado -= Time.deltaTime;
        }
    }

    public void Acelerar()
    {
        Rapidez += Aceleración * Transición_Aceleración;
    }

    //public void Desacelerar()
    //{
    //    Rapidez -= Aceleración * 2f * Transición_Desaceleración;
    //}

    public void Parar()
    {
        Rapidez = 0;
    }
}
