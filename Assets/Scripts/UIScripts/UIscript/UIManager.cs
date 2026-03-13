using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public  void StartGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Show(GameObject Panel)
    {
        Panel.SetActive(true);
    }
    public void Close(GameObject Panel)
    {
        Panel.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
