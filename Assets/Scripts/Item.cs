using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;
    [TextArea]
    public string description;
    
    

    public void ShowItemDescription()
    {
        FindAnyObjectByType<InventoryController>()
            .ShowDescription(description);
    }
}