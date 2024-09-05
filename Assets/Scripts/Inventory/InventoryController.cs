using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //this is the controller for the inventory panel's control.
    public GameObject timerManager;
    [SerializeField]
    private InventoryPage inventoryUI;
    public static InventoryController instance;
    [HideInInspector]
    public int inventorySize = 3;

    void Awake()
    {
        instance = this;
    }
    public void Start() 
    {
    }

    public void ShowInventory()
    {
        inventoryUI.Show();
        inventoryUI.InitializeInventoryUI(3,true);
    }
    public void HideInventory()
    {
        inventoryUI.Hide();
    }
    public void Update() 
    {
        // if (!timer.running)
        // {
        //     inventoryUI.Show();
        // }
        // else
        // {     
        //     inventoryUI.Hide();
        // }
    }
}
