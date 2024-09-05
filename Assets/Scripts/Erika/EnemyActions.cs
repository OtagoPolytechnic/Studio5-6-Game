using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    /// <summary>
    /// Numerator for different enemy types
    /// </summary>
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
    private const float WAIT_TIME = 1f;

    //ranged enemy variables
    [SerializeField] private GameObject bullet;

    // assign the player object from the scene to this in the prefab
    // so that it doesn't have to search through every object in the scene
    private GameObject player;
    private Coroutine meleeCoroutine;
    private Coroutine rangedCoroutine;


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

        if (distance <= ATTACK_RANGE) // if the enemy is within range of the player
        {
            switch (enemyType) // switch statement for checking type of enemy for modular structure
            {
                case EnemyTypes.Ranged:
                    if (rangedCoroutine == null) // if the coroutine is not running
                    {
                        rangedCoroutine = StartCoroutine(RangedAttack()); // start the ranged attack coroutine
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (rangedCoroutine != null)
            {
                StopCoroutine(rangedCoroutine); // stop the attack coroutine
                rangedCoroutine = null;
            }
        }
        // move towards player
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
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
            yield return new WaitForSeconds(WAIT_TIME); //Attack duration
        }
    }

    /// <summary>
    /// Coroutine for the ranged enemy to attack the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator RangedAttack()
    {
        while(true)
        {
            // attack animation
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(WAIT_TIME); //Attack duration
        }
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
