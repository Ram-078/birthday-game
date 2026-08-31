using UnityEngine;

public class MenuContoller : MonoBehaviour
{
    public GameObject menuCanvas;
    void Start()
    {
        PauseController.SetPause(false);
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!menuCanvas.activeSelf && PauseController.isPaused)
            {
                return;
            }
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
        }
    }
}
