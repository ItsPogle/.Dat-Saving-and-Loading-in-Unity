using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Button randomItemBtn, experienceBtn, clearDataBtn;

    [Header("Inventory")]
    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Sprite[] items;

    void Awake()
    {
        randomItemBtn.onClick.AddListener( delegate { GiveRandomItem(); } );
        experienceBtn.onClick.AddListener( delegate { AddExperience(5); } );
        clearDataBtn.onClick.AddListener( delegate { ClearData(); } );
    }

    void AddExperience(int amount)
    {
        SaveManager.Singleton.playerData.experience += amount;
        experienceText.text = "Experience: " + SaveManager.Singleton.playerData.experience;
    }

    public void LoadData()
    {
        List<int> inventory = SaveManager.Singleton.playerData.inventory;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] != -1)
            { SpawnItem(inventory[i]); }
        }

        experienceText.text = "Experience: " + SaveManager.Singleton.playerData.experience;
    }

    public void SaveInventory()
    {
        List<int> inventoryList = new List<int>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].isFull)
            { inventoryList.Add(inventorySlots[i].itemId); }
            else
            { inventoryList.Add(-1); }
        }

        SaveManager.Singleton.playerData.inventory = inventoryList;
    }

    public void SpawnItem(int id)
    {
        InventorySlot availableSlot = null;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(!inventorySlots[i].isFull)
            { 
                availableSlot = inventorySlots[i];
                break;
            }
        }

        if(availableSlot != null)
        {
            availableSlot.image.sprite = items[id];
            availableSlot.isFull = true;
            availableSlot.itemId = id;
        }
    }

    void GiveRandomItem()
    {
        int random = Random.Range(0, items.Length);
        SpawnItem(random);
    }

    void ClearData()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].isFull)
            { 
                inventorySlots[i].itemId = -1;
                inventorySlots[i].isFull = false;
                inventorySlots[i].image.sprite = null;
            }
        }
        
        SaveManager.Singleton.playerData.experience = 0;
        experienceText.text = "Experience: " + SaveManager.Singleton.playerData.experience;
    }
}
