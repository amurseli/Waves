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