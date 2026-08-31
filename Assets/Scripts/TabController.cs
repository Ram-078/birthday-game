using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabimages;
    public GameObject[] pages;
    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabimages[i].color = Color.grey;
        } 
        pages[index].SetActive(true);
        tabimages[index].color = Color.white;
    }
}
