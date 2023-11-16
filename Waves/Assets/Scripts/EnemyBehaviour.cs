using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TextMeshPro textMeshProComponent;

    private void Start()
    {
        bool textAbove = Random.Range(0, 2) == 0;

        // Adjust the local position of the text
        if (textAbove)
        {
            textMeshProComponent.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
        else
        {
            textMeshProComponent.transform.localPosition = new Vector3(0f, -1f, 0f);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Entro al collision");
        
        if (collision.gameObject.CompareTag("Base"))
        {
            moveSpeed = 0f;
        }
        
    }
}
