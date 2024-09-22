using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected from + " + collision.gameObject.name);
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Detected from + " + collision.gameObject.name);
    }
}
