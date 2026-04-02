using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public Slider slider1;
    public AudioSource audioSource1;

    private void Update()
    {
        audioSource.volume = slider.value;
        audioSource1.volume = slider1.value;
    }
}
