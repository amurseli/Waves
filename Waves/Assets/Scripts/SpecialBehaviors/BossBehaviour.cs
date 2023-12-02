using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour,IBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public bool hitting = false;
    public void move()
    {
        int childCount = transform.childCount;

        // Get the center position of the parent object
        Vector3 centerPosition = transform.position;

        // Rotate each child around the parent
        foreach (Transform child in transform)
        {
            Vector3 offset = child.position - centerPosition;

            offset = Quaternion.Euler(0, 0, 20f * Time.deltaTime) * offset;

            child.position = centerPosition + offset;
            transform.LookAt(transform);
            
        }

        // Calculate the horizontal oscillation using cosine
        float xOffset = Mathf.Cos(Time.time * 1f) * 1f;

        // Apply the modified x and y components
        transform.Translate(new Vector3(xOffset, -1, 0) * Time.deltaTime);
    }

    public bool isHitting()
    {
        return hitting;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            hitting = true;
            moveSpeed = 0f;
        }
    }
}
