using UnityEngine;
using UnityEngine.UI;

public class SemaforoVida : MonoBehaviour
{
    [Header("Configuración UI")]
    public Image imagenSemaforo;
    public Sprite[] estadosSemaforo; 
    
    public GameObject panelOptions;

    [Header("Lógica")]
    public int fallos = 0;

    public void RegistrarFallo()
    {
        fallos++;
        ActualizarVisual();

        if (fallos >= 3)
        {
            PerderJuego();
        }
    }

    private void ActualizarVisual()
    {
        if (fallos < estadosSemaforo.Length)
        {
            imagenSemaforo.sprite = estadosSemaforo[fallos];
        }
    }

    private void PerderJuego()
    {
        panelOptions.gameObject.SetActive(true);
    }

    public void Resetear()
    {
        fallos = 0;
        ActualizarVisual();
    }
}

