using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages an object that can be unlocked by a key.
/// </summary>
/// <remarks>
/// Author: Chase Bennett-Hill
/// Date: 29 / 08 / 2024
/// </remarks>
public class UnlockableObject : MonoBehaviour
{
    /// <summary>
    /// The set of keys that are required to unlock this object.
    /// </summary>
    public List<Key> requiredKeys;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// Whether or not this object is currently locked.
    /// </summary>
    public bool isLocked = true;

    void Awake()
    {
        KeysManager.Instance.AddUnlockableObject(this); //  Adds this object to the KeysManager list of unlockable objects.
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isLocked)
        {
            spriteRenderer.sprite = lockedSprite;
        }

        foreach (Key key in requiredKeys)
        {
            key.targetObject = this;
        }
    }

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
                Debug.Log("Key Obtained for " + gameObject.name + " " + requiredKeys.Count + " keys remaining.");
        }
        else
        {
            Debug.Log("This key does not unlock this object.");
        }
    }


    public void Unlock()
    {
        isLocked = false;
        spriteRenderer.sprite = unlockedSprite;
        Debug.Log("You have unlocked the " + gameObject.name );

    }
}
