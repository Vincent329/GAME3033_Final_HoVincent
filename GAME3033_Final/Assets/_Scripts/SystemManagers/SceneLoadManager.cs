using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;
    public static SceneLoadManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadWinScreen()
    {
        SceneManager.LoadScene("WinScreen");

    }
    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");

    }
}
