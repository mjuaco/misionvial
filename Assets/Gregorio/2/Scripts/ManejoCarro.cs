using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class ManejoCarro : MonoBehaviour
{
    public int Aceleración;
    public int Freno;

    public int Presionado;
    public bool Cambio;

    Rigidbody Rigidbody;

    public List<WheelCollider> Ruedas = new List<WheelCollider>();
    public List<GameObject> RuedasIlustración = new List<GameObject>();
    public GameObject Carroza;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Debug.Log(Ruedas[0].rpm);
        Posiciones();
    }

    void FixedUpdate()
    {
        if (Cambio)
        {
            Acelerar();
        }
        else
        {
            Frenar();
        }
    }

    public void Acelerar()
    {
        foreach (var wheel in Ruedas)
        {
            wheel.motorTorque = Presionado * 600 * Aceleración * Time.deltaTime;
        }
    }

    public void Frenar()
    {
        foreach (var wheel in Ruedas)
        {
            wheel.brakeTorque = Presionado * 600 * Freno * Time.deltaTime;
        }
    }

    public void PresionarAcelerar()
    {
        Presionado = 1;
        if (!Cambio)
        {
            Cambio = true;
        }
    }

    public void PresionarFrenar()
    {
        Presionado = 1;
        if (Cambio)
        {
            Cambio = false;
        }
    }

    public void NoPresionar()
    {
        Presionado = 0;
    }

    public void Posiciones()
    {
        for (int i = 0; i < RuedasIlustración.Count; i++)
        {
            Ruedas[i].GetWorldPose(out Vector3 pos, out Quaternion quat);
            RuedasIlustración[i].transform.position = new Vector3(pos.x, pos.y, -1.780008f);
            RuedasIlustración[i].transform.rotation = Quaternion.Euler(quat.eulerAngles.z, 0, -quat.eulerAngles.x);
        }
        Carroza.transform.position = new Vector3(Rigidbody.transform.position.x,
                                                 Rigidbody.transform.position.y,
                                                 Rigidbody.transform.position.z - 0.9f);
    }
}
