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

    public float GetRarityToPrice(rarity r)
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
            shopItem.SetItem(item);
        }

        for (int i = 0; i < 3; i++)
        {
            StatUpgrade statUpgrade = Instantiate(upgradeUIPrefab, upgradeContainer)
                .GetComponent<StatUpgrade>();
            statUpgrade.Stat = (Stats)i;
        }
    }

    public void ClearUI()
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

    public void UpdateUI()
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

    private void ShowUI(ShopTabs tab)
    {
        switch (tab)
        {
            case ShopTabs.ITEMS:
                itemContainer.gameObject.SetActive(true);
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

    private void LoadUpgrades()
    {
        upgradeBtnImage.color = selected;
        itemsBtnImage.color = unSelected;
        ShowUI(ShopTabs.UPGRADES);
    }

    private void LoadItems()
    {
        upgradeBtnImage.color = unSelected;
        itemsBtnImage.color = selected;
        ShowUI(ShopTabs.ITEMS);

    }

    public void SetUpgradesActive()
    {
        activeTab = ShopTabs.UPGRADES;
    }

    public void SetItemsActive()
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

    public void OpenShop()
    {
        // InventoryController.instance.ShowInventory();
        panel.SetActive(true);
        ShopMenuOpen = true;
    }

    public void CloseShop()
    {
        // InventoryController.instance.HideInventory();
        ShopMenuOpen = false;
        panel.SetActive(false);
    }
}
