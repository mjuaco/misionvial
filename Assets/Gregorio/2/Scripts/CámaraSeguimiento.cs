using UnityEngine;

public class CámaraSeguimiento : MonoBehaviour
{
    public Transform Carro;

    void Update()
    {
        Camera.main.transform.position = new Vector3(Carro.position.x + 15, Carro.position.y + 22.5f, Camera.main.transform.position.z);
    }
}
