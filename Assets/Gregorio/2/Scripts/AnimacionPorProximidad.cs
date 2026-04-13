using UnityEngine;

public class AnimacionPorProximidad : MonoBehaviour
{
    public Animator _animator;
    [SerializeField] private string nombreTrigger = "tren";
    [SerializeField] private string tagObjetivo = "Carro";

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObjetivo))
        {
            if (_animator != null)
            {
                _animator.SetTrigger(nombreTrigger);
                Debug.Log("Jugador cerca: Activando animación.");
            }
        }
    }

}