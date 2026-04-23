using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform targetPlayer;
    private float speed;
    private GameManager gameManager;
    
    public AudioClip screamSound;

    // Called by the spawner right after creation
    public void Initialize(Transform player, float moveSpeed, GameManager manager)
    {
        targetPlayer = player;
        speed = moveSpeed;
        gameManager = manager;
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            // Move smoothly towards the player's position
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
            
            // Make the enemy look at the player
            transform.LookAt(targetPlayer);
        }
    }

    // Handles collisions with the Sword and the Player Body
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            // Play scream sound at this position right before destroying
            AudioSource.PlayClipAtPoint(screamSound, transform.position);
            
            // Add any particle effects here
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("PlayerBody"))
        {
            // The enemy touched the player! End the game.
            gameManager.GameOver();
        }
    }
}