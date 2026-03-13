using UnityEngine;
using TMPro;
public class UserProfiel : MonoBehaviour
{
    public TMP_InputField nameField;
    public TextMeshProUGUI nameText;

    public void Update()
    {
        nameText.text = Profiel.username;
    }
    public void SaveName()
    {
        string userName = nameField.text;

        if (!string.IsNullOrEmpty(userName))
        {
            PlayerPrefs.SetString("USERNAME", userName);
            PlayerPrefs.Save();
        }
        nameField.text = " ";
    }
}

public static class Profiel
{
    public static string username => PlayerPrefs.GetString("USERNAME", "Player");
}