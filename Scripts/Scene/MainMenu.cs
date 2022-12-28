using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("Killhouse");
        Time.timeScale = 1.0f;
        Debug.Log("Game Start!");
    }

    public void OnClickStartTestMap()
    {
        SceneManager.LoadScene("TestMap");
        Time.timeScale = 1.0f;
        Debug.Log("Start Test map!");
    }

    public void OnClickHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        Debug.Log("How To Play?");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
