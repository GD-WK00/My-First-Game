using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float speed = 15.0f;
    private float yDestroy = -10.0f;

    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        DestroyBody();
    }

    void EnemyMovement()
    {
        Vector3 chasePlayer = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(chasePlayer * speed);
    }

    void DestroyBody()
    {
        if(transform.position.y < yDestroy)
        {
            Destroy(gameObject);
        }
    }
}
