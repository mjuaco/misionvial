using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Seguimiento : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public float límite;
    public int límite_máximo;
    public float longitud = 5f;
    private float Aceleración = 0.2f;
    private float rapidez;
    public float velocidad;
    public int velocidad_máxima;
    public GameObject objecto;
    private Rigidbody rb;

    public bool doblar;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Detección();
        Debug.Log(transform.localRotation.eulerAngles);
    }

    private void FixedUpdate()
    {
        velocidad = Mathf.Clamp(rapidez, 0f, velocidad_máxima);
        rb.linearVelocity = transform.TransformDirection(new Vector3(velocidad, 0f));
        if (hit.collider == null && velocidad < velocidad_máxima)
        {
            Acelerar();
            //Debug.Log("No Hit");
        }
        else if (hit.collider != null && velocidad > 0)
        {
            Desacelerar();
            //Debug.Log("Hit");
        }

        if (doblar)
        {
            StartCoroutine(Doblar_Derecha());
        }
    }
    public void Acelerar()
    {
        rapidez += Aceleración;
    }
    public void Desacelerar()
    {
        rapidez -= Aceleración;
    }
    public void Detección()
    {
        límite = Mathf.Clamp(longitud, 2f, límite_máximo);
        longitud = límite_máximo * (velocidad / velocidad_máxima);
        ray = new Ray(objecto.transform.position, transform.TransformDirection(Vector3.right));
        Physics.Raycast(ray, out hit, límite);
        Debug.DrawRay(ray.origin, ray.direction * límite);
    }

    public IEnumerator Doblar_Derecha()
    {
        float tiempo = 0f;
        while (tiempo <= 2f)
        {
            float transición = Mathf.Lerp(0, 90, tiempo);
            transform.eulerAngles += new Vector3(0f, transición, 0f);
            tiempo += Time.deltaTime;
            yield return null;
        }
    }
}
