using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Vector3 playerPosition;

    public string currentBoundaryName;
    public HashSet<string> defeatedBosses = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ResetLevelData()
    {
        playerPosition = Vector3.zero;
        currentBoundaryName = "";
        defeatedBosses.Clear();
    }
}