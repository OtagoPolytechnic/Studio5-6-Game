using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// This class manages an object that can be unlocked by a key.
/// </summary>
/// <remarks>
/// Author: Chase Bennett-Hill
/// Date: 29 / 08 / 2024
/// </remarks>
/// 
public class UnlockableObject : MonoBehaviour
{
    /// <summary>
    /// The set of keys that are required to unlock this object.
    /// </summary>
    public List<Key> requiredKeys;

    /// <summary>
    /// The sprite that is displayed when the object is unlocked.
    /// </summary>
    [SerializeField] private Sprite unlockedSprite;

    /// <summary>
    ///     The sprite that is displayed when the object is locked.
    /// </summary>
    [SerializeField] private Sprite lockedSprite;

    /// <summary>
    /// The sprite renderer component of this object.
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// Whether or not this object is currently locked.
    /// </summary>
    public bool isLocked = true;

    [SerializeField] private TMP_Text keyText;
    [SerializeField] private GameObject doorPromptPanel;

    void Awake()
    {
        KeysManager.Instance.AddUnlockableObject(this); //  Adds this object to the KeysManager list of unlockable objects.
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (isLocked)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
            spriteRenderer.sprite = lockedSprite;
        }

        foreach (Key key in requiredKeys)
        {
            key.targetObject = this;
        }
    }


    /// <summary>
    /// Uses a key on the object if the key is in the required keys list.
    /// </summary>
    /// <param name="key">Which key is going to be used</param>
    public void UseKey(Key key)
    {
        if (requiredKeys.Contains(key))
        {
            requiredKeys.Remove(key);
            Destroy(key.gameObject);
            if (requiredKeys.Count == 0)
            {
                keyText.text = "Door Unlocked!";
                Unlock();

            }
            else
            {
                keyText.text = "Keys remaining: " + requiredKeys.Count;
            }

        }
        else
        {
        }
    }


    /// <summary>
    /// When all keys are used, the object is unlocked.
    /// </summary>
    public void Unlock()
    {
        isLocked = false;
        spriteRenderer.enabled = false;
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (gameObject.name == "Boss Room Door")
        {
            MapOverlay.instance.UnlockBossRoom();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            doorPromptPanel.SetActive(true);
        }
      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            doorPromptPanel.SetActive(false);
        }
    }
}
