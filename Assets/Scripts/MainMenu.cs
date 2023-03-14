using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //voids for buttons
    public void Logs()
    {
        SceneManager.LoadScene("logs");
    }

    public void Info()
    {
        SceneManager.LoadScene("info");
    }

    public void Debug()
    {
        SceneManager.LoadScene("debug");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }

}
