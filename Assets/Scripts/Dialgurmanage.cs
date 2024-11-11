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
        yield return new WaitForSeconds(5);
        dialogueBox.SetActive(false);
    }

}
