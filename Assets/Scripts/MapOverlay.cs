using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOverlay : MonoBehaviour
{
    private GameObject mainMap;
    public static MapOverlay instance;
    [SerializeField] private GameObject bossDoorLocked;

    void Awake()
    {
        instance = this;
    }   
    // Start is called before the first frame update
    void Start()
    {
        mainMap = gameObject.transform.GetChild(0).gameObject;
        mainMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.M))
        {
            mainMap.SetActive(!mainMap.activeSelf);
        }
    }
    public void UnlockBossRoom()
    {
        bossDoorLocked.SetActive(false);
    }
}
