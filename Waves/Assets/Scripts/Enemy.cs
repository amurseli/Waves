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

    public bool hitCheck(string word)
    {
        if (word == targetWord)
        {
            // Get all the child objects of the parent
            Transform parentTransform = transform.parent;
            int siblingCount = parentTransform.childCount;

            bool otherWordsExist = false;

            // Check if there are other words besides the current one
            for (int i = 0; i < siblingCount; i++)
            {
                if (parentTransform.GetChild(i) != transform)
                {
                    otherWordsExist = true;
                    break;
                }
            }

            // Process damage based on conditions
            if (otherWordsExist)
            {
                // There are other words, destroy only the current object
                Destroy(gameObject);
            }
            else
            {
                // No other words, destroy the parent object
                Destroy(parentTransform.gameObject);
            }

            return true;
        }

        return false;
    }
    
    public void AssignWord(string word)
    {
        targetWord = word;
        textMeshProComponent.text = targetWord;
    }
    
    void OnDestroy()
    {
        // Cuando el objeto Enemy se destruye, elimínalo de la lista en el script writer
        writer script = GameObject.Find("Writer").GetComponent<writer>();
        if (script != null)
        {
            script.RemoveEnemyFromList(this);
        }
    }
    
}
