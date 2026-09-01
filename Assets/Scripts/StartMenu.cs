using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
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
        UnityEngine.Application.OpenURL("javascript:window.location.reload();");
        #else
        Application.Quit();
        #endif
    }
}
