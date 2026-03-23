using UnityEngine;

public class Generar : MonoBehaviour
{
    public GeneradorNiveles GeneradorNiveles;
    void Start()
    {
        GeneradorNiveles = GameObject.Find("GameManager").GetComponent<GeneradorNiveles>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carro"))
        {
            GeneradorNiveles.Actualización();
            this.gameObject.SetActive(false);
        }
    }
}
