using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{ 
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;
    public GameObject descriptionPanel;
    public TMP_Text descriptionText;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
           Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
           if (i < itemPrefabs.Length)
           {
               GameObject item = Instantiate(itemPrefabs[i], slot.transform);
               item.GetComponent<RectTransform>().anchoredPosition =  Vector2.zero;
               slot.currentItem = item;
           }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        int index = 0;

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            
            if (slot != null && slot.currentItem == null)
            {
                GameObject newitem = Instantiate(itemPrefab, slot.transform);
                newitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newitem;
                return true;
            }

            index++;
        }

        Debug.Log("Inventory Full!");
        return false;
    }
    public void ShowDescription(string description)
    {
        descriptionPanel.SetActive(true);
        descriptionText.text = description;
    }
    
    

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideDescription();
        }
    }
    
}
