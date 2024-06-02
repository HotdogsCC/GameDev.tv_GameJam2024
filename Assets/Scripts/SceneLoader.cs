using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLoseScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        
    }
}
