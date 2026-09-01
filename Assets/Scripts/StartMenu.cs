using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class StartMenu : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void QuitGame();
    #endif
    
    public GameObject startMenu;
    public GameObject InstructionsMenu;
    
    
    public void OnStart()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void Instructions()
    {
        startMenu.SetActive(false);
        InstructionsMenu.SetActive(true);
    }
    
    public void onHome()
    {
        startMenu.SetActive(true);
        InstructionsMenu.SetActive(false);
    }

    public void OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
        QuitGame();
        #else
        Application.Quit();
        #endif
    }
}
