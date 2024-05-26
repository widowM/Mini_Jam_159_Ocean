using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static bool _isPaused = false;
    public static bool IsPaused => _isPaused;
    public void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes in build settings.");
        }
    }
    //
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        EnableCursor();
        Time.timeScale = 0f;
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
    }
}