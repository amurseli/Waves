using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalMovement : MonoBehaviour,IBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public bool hitting = false;
    public void move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
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
