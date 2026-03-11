using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
