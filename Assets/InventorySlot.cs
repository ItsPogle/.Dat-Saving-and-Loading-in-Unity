using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isFull { get; set; }
    public int itemId { get; set; } = -1;
    
    public Image image;
}
