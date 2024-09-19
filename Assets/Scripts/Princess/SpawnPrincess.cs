using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrincess : MonoBehaviour
{
    public Princess princess;


    //call in Boss script when boss dies
    public void InstantiatePrincess()
    {
        Instantiate(princess, transform.position, Quaternion.identity);
    }
}
