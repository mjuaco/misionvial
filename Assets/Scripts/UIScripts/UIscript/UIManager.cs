using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public  void StartGamePlay(string scene)
    {
        SceneManager.LoadScene(scene);
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

    public void pause()
    {
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
    }
}
