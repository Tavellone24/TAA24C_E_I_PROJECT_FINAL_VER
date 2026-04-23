using UnityEngine;

public class CatchZone : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform swordRespawnPoint;

    // This runs automatically when an object with a Rigidbody enters the Trigger box
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that fell in is the Sword
        // (This uses the "Sword" tag you set up previously)
        if (other.CompareTag("Sword"))
        {
            // 1. Move the sword to the spawn point
            other.transform.position = swordRespawnPoint.position;
            other.transform.rotation = swordRespawnPoint.rotation;

            // 2. Kill its physics momentum
            // If we don't do this, it will respawn but keep falling at terminal velocity!
            Rigidbody swordRb = other.GetComponent<Rigidbody>();
            if (swordRb != null)
            {
                swordRb.linearVelocity = Vector3.zero;
                swordRb.angularVelocity = Vector3.zero;
            }
        }
    }
}