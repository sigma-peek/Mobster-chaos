using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float enemyHealth = 100f, enemyDamage = 5f;

    private float enemySpeed = 6f, stopDistance = 10f; 
    private Transform player;

    private bool enemyCooldown = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GetShot(float damage)
    {
        enemyHealth -= damage;

        Debug.Log(string.Format("Enemy health: {0}", enemyHealth));

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }

        else
        {
            ShootAtPlayer();
        }

        /*
        if (Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            ShootAtPlayer();
        }
        */
    }

    private void ShootAtPlayer()
    {
        if (!enemyCooldown)
        {
            RaycastHit hitPlayer;

            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hitPlayer, Mathf.Infinity))
            {
                if (hitPlayer.collider.CompareTag("Player"))
                {
                    //Debug.Log("Player is hit");

                    PlayerMovement playerHealth = hitPlayer.collider.GetComponent<PlayerMovement>();
                    //Debug.Log("Player is hit");

                    if (playerHealth != null)
                    {
                        //Debug.Log("Player is hit");
                        playerHealth.TakeDamage(enemyDamage);
                        //StartEnemyCooldown();
                        StartCoroutine(StartEnemyCooldown());
                    }

                }
            }
        }
      
    }

    private IEnumerator StartEnemyCooldown()
    {
        // Set cooldown state to true
        enemyCooldown = true;
        // Wait for cooldown duration
        yield return new WaitForSeconds(0.5f);
        // Reset cooldown state
        enemyCooldown = false;
    }
}
