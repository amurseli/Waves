using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stopY = -5f; // Adjust this value to set the point where enemies should stop

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entro al collision");
        if (collision.gameObject.CompareTag("Base"))
        {
            Debug.Log("Entro a Base");
            moveSpeed = 0f;
        }
    }
}
