using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool hitting = false;
    private bool canHit = true;
    private GameObject enemyBase;
    private float enemyHp;

    private void Start()
    {
        enemyBase = GameObject.FindWithTag("Base");
    }

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if (hitting && canHit)
        {
            StartCoroutine(WaitAndHit());
        }
        if (transform.childCount == 0)
        {
            // Destroy the object if it has no children
            Destroy(gameObject);
        }
        RotateChildren();
    }
    
    void RotateChildren()
    {
        if (transform.childCount > 2)
        {
            // Get the center position of the parent object
            Vector3 centerPosition = transform.position;

            // Rotate each child around the parent
            foreach (Transform child in transform)
            {
                // Calculate the position offset from the center
                Vector3 offset = child.position - centerPosition;

                // Rotate the offset
                offset = Quaternion.Euler(0, 0, 20f * Time.deltaTime) * offset;

                // Set the new position of the child
                child.position = centerPosition + offset;
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            hitting = true;
            moveSpeed = 0f;
        }
    }

    private IEnumerator WaitAndHit()
    {
        canHit = false;
        yield return new WaitForSeconds(0.5f);
        if (enemyBase != null)
        {
            Base baseScript = enemyBase.GetComponent<Base>();

            if (baseScript != null)
            {
                baseScript.hit(10f);
            }
        }
        yield return new WaitForSeconds(3f);
        canHit = true;
    }
}