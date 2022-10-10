using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private float speed = 25.0f;
    private float jumpForce = 100.0f;
    private float yDestroy = -10.0f;
    private bool onGround = true;
    private int gemCount = 3;
    private bool gameOver = false;

    private Rigidbody playerRb;
    public ParticleSystem fireworks;
    private AudioSource jumpSound;

    public TextMeshProUGUI gemsLeft;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWonText;

    //Variables for spawning enemies (Spawn Manager)
    private float axisLimit = 4.0f;
    private  bool enemyAppear = false;
    private int enemyCount;
    private int gemPickedNumber = 1;

    public GameObject enemy;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();
        DestroyBody();
        SpawnManager();
    }


    //Allows move based on arrow keys
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    //Allows player jumping by pressing the spacebar when he is on ground
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !enemyAppear && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            jumpSound.Play();
        }
    }

    //Destroys player if he fall of the platform
    void DestroyBody()
    {
        if (transform.position.y < yDestroy)
        {
            Destroy(gameObject);
            gameOverText.gameObject.SetActive(true);
            gameOver = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //enemy spawn trigger
        if (other.gameObject.CompareTag("Gemstone"))
        {
            Destroy(other.gameObject);
            enemyAppear = true;
            gemCount--;
            gemsLeft.text = "Gems left: " + gemCount;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if player is not on the ground, then he cannot jump again
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (gemCount == 0)
            {
                fireworks.Play();
                gameWonText.gameObject.SetActive(true);
            }
        }  
    }

    //Spawn Manager functions

    //set a random position around a player to spawn enemy/enemies
    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = (transform.position.x + Random.Range(-axisLimit, axisLimit));
        float spawnPosZ = (transform.position.z + Random.Range(-axisLimit, axisLimit));

        Vector3 randomPos = new Vector3(spawnPosX, 3f, spawnPosZ);

        return randomPos;
    }

    //Main function to spawning enemies after picking gems
    private void SpawnManager()
    {
        if (enemyAppear && enemyCount < 1)
        {
            SpawnEnemy(gemPickedNumber);
            gemPickedNumber++;
        }

        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
            enemyAppear = false;
    }

    //generate enemies based on how many gems player picked in the game
    private void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemy, GenerateSpawnPos(), enemy.transform.rotation);
        }
    }
}
