using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialgurmanage : MonoBehaviour
{
    public static Dialgurmanage i;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        i = this;
    }

    public void ShowDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.fontSize = 15;
        dialogueText.text = message;
        StartCoroutine(HideDialogueAfterDelay(5f));
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        Debug.Log("bug");
        yield return new WaitForSeconds(5);
        Debug.Log("bug123");
        dialogueBox.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
