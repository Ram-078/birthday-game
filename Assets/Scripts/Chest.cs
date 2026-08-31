using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour, Interactable
{
    public bool isOpened{get; private set;}
    public string ChestID{get; private set;}
    
    [SerializeField] private GameObject itemPrefab;

    public Sprite openedSprite;

    void Start()
    {
        ChestID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

   

    public void Interact()
    {
        if(!CanInteract()) return;
        OpenChest();
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    private void OpenChest()
    {
        SetOpened(true);
        SoundFxManager.Play("ChestOpen");

        if (itemPrefab)
        {
            GameObject droppeditem = Instantiate(itemPrefab, transform.position+ Vector3.down*1.31f, Quaternion.identity);

        }
        
    }

   
    public void SetOpened(bool opened)
    {
        isOpened = opened;
        if (isOpened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
    
}
