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
            //Procesar el daño
            Destroy(transform.parent.gameObject);
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
