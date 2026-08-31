using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPaused { get; private set; }= false;

    public static void SetPause(bool pause)
    {
        isPaused = pause;
    }
}
