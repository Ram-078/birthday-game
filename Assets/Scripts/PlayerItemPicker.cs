using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerItemPicker : MonoBehaviour
{
    private InventoryController inventoryController;

    public string nextScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = GameObject.FindAnyObjectByType<InventoryController>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       StartCoroutine(ItemPickup(collision));
    }

    IEnumerator ItemPickup(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    
                    Destroy(collision.gameObject);
                    yield return new WaitForSeconds(4f);
                    SaveManager.Instance.ResetLevelData();
                    SceneManager.LoadScene(nextScene);
                }
            }
        }
    }
}


