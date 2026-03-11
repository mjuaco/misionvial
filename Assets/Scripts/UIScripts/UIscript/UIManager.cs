using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void Start()
    {

    }

    private void StartGamePlay()
    {

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
