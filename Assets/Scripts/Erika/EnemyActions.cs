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
    private const float ATTACK_RANGE = 10f;

    //ranged enemy variables
    [SerializeField] private GameObject bullet;

    // assign the player object from the scene to this in the prefab
    // so that it doesn't have to search through every object in the scene
    [SerializeField] private GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

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
                case EnemyTypes.Melee:
                    // StartCoroutine(MeleeAttack());
                    break;
                case EnemyTypes.Ranged:
                    // StartCoroutine(RangedAttack());
                    break;
                default:
                    break;
            }
        }
        else
        {
            // move towards player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (1f) * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    // !!!!
    // The enemy type check is done in movement, so that it doesn't keep checking every time the coroutine is called
    // !!!!

    private IEnumerator MeleeAttack()
    {
        // attack animation
        yield return new WaitForSeconds(1f); //Attack duration
    }

    // both the melee enemy and the bullet prefab
    // could use a scrip that deals damage to the player
    // by first checking the enemy type
    // enemy types could use a struct so that it is assigned health and damage values

    private IEnumerator RangedAttack()
    {
        // attack animation
        Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f); //Attack duration
    }

    // !!!!
    // this coroutine checks which enemy type in the coroutine, negating the need for multiple coroutines
    // !!!!

    // private IEnumerator Attack()
    // {
    //     switch (enemyType)
    //     {
    //         case EnemyTypes.Melee:
    //             break;
    //         case EnemyTypes.Ranged:
    //             break;
    //         default:
    //             break;
    //     }
    //     yield return new WaitForSeconds(1f); //Attack duration
    // }
}
