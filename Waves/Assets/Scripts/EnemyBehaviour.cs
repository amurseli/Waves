using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stopY = -5f; // Adjust this value to set the point where enemies should stop

    private void Update()
    {
        // Move the enemy down
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the stop point
        if (transform.position.y <= stopY)
        {
            // Stop the enemy's movement
            moveSpeed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy has collided with another object (e.g., a barrier)
        if (collision.gameObject.CompareTag("Barrier"))
        {
            // Stop the enemy's movement when colliding with the barrier
            moveSpeed = 0f;
        }
    }
}
