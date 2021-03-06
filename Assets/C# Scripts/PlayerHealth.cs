using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Life point of the player and behavoir related to loosing the health points.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public static float health;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private bool hitEnemy = false;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    public static bool playerDead = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        playerPosition = GetComponent<Transform>();
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Dictate the behaviors of the player character when 
    /// they take damages and when they loses all health point.
    /// </summary>
    /// <param name="damage"></param>
    /// <author>Collin Williams</author>
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        //player alive
        if (currentHealth > 0)
        {

        } 
        //player dead
        else
        {
            spriteRenderer.color = Color.black;
            playerDead = true;
            playerPosition.position = new Vector3(-999,-999,-999);

            SceneManager.LoadScene("Game Over");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth.health = currentHealth;
        //if the player falls off the map
        if (playerPosition.position.y < -15)
        {
            TakeDamage(1);
        } 
        else if (hitEnemy)
        {
            if (body.mass != 100)
            {
                TakeDamage(1f);
                hitEnemy = false;
            } 
        }
    }

    /// <summary>
    /// Dictate if the player hit the enemies.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    { 

        //If the player has run into the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            hitEnemy = true;
        }
        else
        {
            hitEnemy = false;
        }
    }
}
