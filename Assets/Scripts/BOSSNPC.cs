using UnityEngine;

public class BossNPC : MonoBehaviour
{
    public string bossID;
    public string battleSceneName;
    void Start()
    {
        if (SaveManager.Instance.defeatedBosses.Contains(bossID))
        {
            gameObject.SetActive(false);
        }
    }
}