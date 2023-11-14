using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro textMeshProComponent;
    public GameObject writer;
    private string targetWord;

    void Start()
    {
        GameObject writer = GameObject.Find("Writer");

        if (writer != null)
        {
            writer anotherScript = writer.GetComponent<writer>();
           
            if (anotherScript != null)
            {
                while (targetWord == null)
                {
                    targetWord = anotherScript.GetRandomWordAndRemove();
                
                }
                textMeshProComponent.text = targetWord;
            }
            else
            {
                Debug.LogWarning("AnotherScript component not found on the other object.");
            }
        }
        else
        {
            Debug.LogWarning("Object with AnotherScript not found in the scene.");
        }
    }

    public void hitCheck(string word)
    {
        if (word == targetWord)
        {
            //Procesar el daño
            Destroy(transform.parent.gameObject);
        }
    }
    
}
