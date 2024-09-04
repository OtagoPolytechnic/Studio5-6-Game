using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Serializable]
// public struct EnemyTypes
// {
   
// }

/*
* Attached to every enemy
* Must have a movement method
* Attack method that contains two different types of attacks

*/


public class EnemyActions : MonoBehaviour
{
    enum EnemyTypes
    {
        Melee,
        Ranged
        //fast,
        //heavy
    };

    [SerializeField] private EnemyTypes enemyType; //creates a drop down menu in the inspector
    private float distance;
    private Vector2 direction;
    private float angle;
    private bool isAttacking = false;
    private const float ATTACK_RANGE = 10f;

    //ranged enemy variables
    [SerializeField] private GameObject bullet;

    // assign the player object from the scene to this in the prefab
    // so that it doesn't have to search through every object in the scene
    private GameObject player;
    private Coroutine meleeCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Moves the enemy towards the player
    /// </summary>
    private void Movement()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance <= ATTACK_RANGE)
        {
            switch (enemyType)
            {
                case EnemyTypes.Ranged:
                    // StartCoroutine(RangedAttack());
                    break;
                default:
                    break;
            }
        }
        //else
        {
            // move towards player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (2f) * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    /// <summary>
    /// Coroutine for the melee enemy to attack the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleeAttack()
    {
        while(true)
        {
            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.EnemyBiteSound();
            }
            PlayerHealth.instance.ReceiveDamage(10);
            yield return new WaitForSeconds(1f); //Attack duration
        }
    }

    /// <summary>
    /// Coroutine for the ranged enemy to attack the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator RangedAttack()
    {
        // attack animation
        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f); //Attack duration
    }

    /// <summary>
    /// Checks if the player is in range of the melee enemy who attacks on contact
    /// </summary>
    /// <param name="other"> The other game object </param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && enemyType == EnemyTypes.Melee)
        {
            if (meleeCoroutine == null)
            {
                meleeCoroutine = StartCoroutine(MeleeAttack());
            }
        }
    }

    /// <summary>
    /// Stops the melee enemy from attacking when the player is out of range
    /// </summary>
    /// <param name="other"> The other game object </param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && enemyType == EnemyTypes.Melee)
        {
            if (meleeCoroutine != null)
            {
                StopCoroutine(meleeCoroutine);
                meleeCoroutine = null;
            }
        }
    }
}
