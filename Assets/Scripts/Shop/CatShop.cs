using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopTabs //Tabs available in the shop
{
    ITEMS,
    UPGRADES
}

public class CatShop : MonoBehaviour
{
    private bool playerInShop = false;
    private bool shopMenuOpen = false;
    public bool ShopMenuOpen
    {
        get => shopMenuOpen;
        set => shopMenuOpen = value;
    }
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
    public Transform upgradeContainer;
    public Transform itemContainer;

    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        instance = this;
    }

    public float GetRarityToPrice(rarity r) //Get the price of an item based on its rarity
    {
        float price = 0;
        switch (r)
        {
            case rarity.Common:
                price = 15;
                break;
            case rarity.Uncommon:
                price = 20;
                break;
            case rarity.Rare:
                price = 25;
                break;
            case rarity.Epic:
                price = 35;
                break;
        }
        return price;
    }
    void Start()
    {
        ClearUI();
        UpdateUI();
        foreach (ItemData item in items)
        {
            ShopItem shopItem = Instantiate(itemUIPrefab, itemContainer).GetComponent<ShopItem>();
            shopItem.SetItem(item); //Iterate through the items and set them in the shop
        }

        for (int i = 0; i < 3; i++)
        {
            StatUpgrade statUpgrade = Instantiate(upgradeUIPrefab, upgradeContainer)
                .GetComponent<StatUpgrade>();
            statUpgrade.Stat = (Stats)i; //iterates for 3 times and sets the stats in the shop based on the index
        }
    }

    public void ClearUI() //Used to clear the template items in the UI on run time
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in upgradeContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateUI() //Update the UI based on the active tab
    {
        switch (activeTab)
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

    private void ShowUI(ShopTabs tab) //Show the UI based on the tab
    {
        switch (tab)
        {
            case ShopTabs.ITEMS:
                itemContainer.gameObject.SetActive(true);
                itemContainer.gameObject.transform.parent.gameObject.transform.Find("Scrollbar").GetComponent<Scrollbar>().value = 1;
                upgradeContainer.gameObject.SetActive(false);
                break;
            case ShopTabs.UPGRADES:
                itemContainer.gameObject.SetActive(false);
                upgradeContainer.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void LoadUpgrades() //Load the upgrades in the shop
    {
        upgradeBtnImage.color = selected;
        itemsBtnImage.color = unSelected;
        ShowUI(ShopTabs.UPGRADES);
    }

     private void LoadItems() //Load the items in the shop
    {
        upgradeBtnImage.color = unSelected;
        itemsBtnImage.color = selected;
        ShowUI(ShopTabs.ITEMS);

    }

    public void SetUpgradesActive() //Set the upgrades tab active
    {
        activeTab = ShopTabs.UPGRADES;
    }

    public void SetItemsActive() //Set the items tab active
    {
        activeTab = ShopTabs.ITEMS;
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
                if (!ShopMenuOpen)
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

    public void OpenShop() //Open the shop
    {
        panel.SetActive(true);
        ShopMenuOpen = true;
    }

    public void CloseShop() //Close the shop
    {
        ShopMenuOpen = false;
        panel.SetActive(false);
    }
}
