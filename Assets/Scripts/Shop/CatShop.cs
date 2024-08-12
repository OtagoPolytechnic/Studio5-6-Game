using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopTabs
{
    ITEMS,
    UPGRADES
}
public class CatShop : MonoBehaviour
{

    private bool playerInShop = false;
    private bool shopMenuOpen = false;
    public static CatShop instance;
    private ShopTabs activeTab = ShopTabs.UPGRADES;
    [SerializeField]
    private Image upgradeBtnImage;
    [SerializeField]
    private Image itemsBtnImage;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Color selected;
        [SerializeField]
    private Color unSelected;

    public GameObject upgradeUIPrefab;
    public GameObject itemUIPrefab;
    public Transform containerParent;


    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        switch(activeTab)
        {
            case ShopTabs.ITEMS:
                LoadItems();
                break;
            case ShopTabs.UPGRADES:
                LoadUpgrades();
                break;
            default:
                break;
        }
    }

    private void ClearUI()
    {
        while (containerParent.childCount > 0) {
        DestroyImmediate(containerParent.GetChild(0).gameObject);
        }

    }
    private void LoadUpgrades()
    {
        upgradeBtnImage.color = selected;
        itemsBtnImage.color = unSelected;
        ClearUI();
        for (int i = 0; i < 3; i++)
        {
            StatUpgrade statUpgrade = Instantiate(upgradeUIPrefab,containerParent).GetComponent<StatUpgrade>();
            statUpgrade.stat = (Stats)i;
        }
    }
    private void LoadItems()
    {
        upgradeBtnImage.color = unSelected;
        itemsBtnImage.color = selected;
                ClearUI();

        Instantiate(itemUIPrefab,containerParent);
    }
    public void SetUpgradesActive()
    {
        activeTab = ShopTabs.UPGRADES;
    }

    public void SetItemsActive()
    {
        activeTab = ShopTabs.ITEMS;
    }
    void Awake()
    {
        instance = this;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerInShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInShop = false;
        }
    }
    void Update()
    {
        if (playerInShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!shopMenuOpen)
                {
                    OpenShop();
                }
                else
                {
                    CloseShop();
                }
            }
        }
    }

    public void OpenShop()
    {
        // InventoryController.instance.ShowInventory();
        panel.SetActive(true);
        shopMenuOpen = true;
    }
    public void CloseShop()
    {
        // InventoryController.instance.HideInventory();
        shopMenuOpen = false;
        panel.SetActive(false);

    }
}
