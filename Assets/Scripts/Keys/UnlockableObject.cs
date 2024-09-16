using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages an object that can be unlocked by a key.
/// </summary>
public class UnlockableObject : MonoBehaviour
{
    public List<Key> requiredKeys = new List<Key>(); // Keys needed to unlock this object
    public bool isLocked = true; // Whether the object is currently locked
    [SerializeField] private Sprite unlockedSprite; // Sprite displayed when the object is unlocked
    [SerializeField] private Sprite lockedSprite; // Sprite displayed when the object is locked

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (isLocked)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = lockedSprite;
            }
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }
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
                Unlock();
            }
            else
            {
                Debug.Log("Key obtained for " + gameObject.name + ". " + requiredKeys.Count + " keys remaining.");
            }
        }
        else
        {
            Debug.Log("This key does not unlock this object.");
        }
    }

    /// <summary>
    /// When all keys are used, the object is unlocked.
    /// </summary>
    public void Unlock()
    {
        isLocked = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        Debug.Log("You have unlocked the " + gameObject.name);
    }
}