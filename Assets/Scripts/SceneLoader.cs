using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // cache references
    GameSessionLogic gameSession;

    public void Start()
    {
        gameSession = FindObjectOfType<GameSessionLogic>();
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            gameSession.SetCurrentLevel(nextSceneIndex);
        }
    }

    public void LoadStartScene()
    {
        gameSession.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void LoadThisScene()
    {
        SceneManager.LoadScene(gameSession.GetCurrentLevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
