using UnityEngine;

public class AnimacionPorProximidad : MonoBehaviour
{
    public Animator _animator;
    public GameObject tren;
    void Start()
    {
        _animator = FindAnyObjectByType<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carro"))
        {
        _animator.SetTrigger("Tren");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Carro"))
        {
            Destroy(tren);
        }
    }

}