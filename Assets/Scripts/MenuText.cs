using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI UIText;
    private string currentText;
    // Start is called before the first frame update
    void Start()
    {
        currentText = UIText.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIText.fontSize += 5;
        UIText.text = "> " + currentText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIText.fontSize -= 5;
        UIText.text = currentText;
    }
}
