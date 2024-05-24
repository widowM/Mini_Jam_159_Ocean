using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}