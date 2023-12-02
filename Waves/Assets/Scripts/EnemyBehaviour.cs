using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool canHit = true;
    private GameObject enemyBase;
    private float enemyHp;
    public bool isBoss = false;
    private IBehaviour _behaviour;

    private void Start()
    {
        enemyBase = GameObject.FindWithTag("Base");
        _behaviour = gameObject.GetComponent<IBehaviour>();
    }

    private void Update()
    {
        _behaviour.move();
        if (_behaviour.isHitting() && canHit)
        {
            StartCoroutine(WaitAndHit());
        }
        if (transform.childCount == 0)
        {
            // Destroy the object if it has no children
            Destroy(gameObject);
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