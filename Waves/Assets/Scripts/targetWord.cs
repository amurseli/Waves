using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class targetWord : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro textMeshProComponent;
    public GameObject writer;
    private string value;

    void Start()
    {
        GameObject writer = GameObject.Find("Writer");

        if (writer != null)
        {
            // Get the "AnotherScript" component from the other object
            writer anotherScript = writer.GetComponent<writer>();
           
            if (anotherScript != null)
            {
                while (value == null)
                {
                    value = anotherScript.GetRandomWordAndRemove();
                
                }
                // Access the public variable "myVariable" from AnotherScript
                

                // Now you can use the "value" variable in your script
                textMeshProComponent.text = value;
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
    
}
