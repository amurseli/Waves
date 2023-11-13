using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class writer : MonoBehaviour
{
    private string inputText;

    private List<string> listOfTarget = new List<string>();

    public List<Enemy> enemies = new List<Enemy>();

    public TextAsset csvFile;

    private void Awake()
    {
        randomWordSelector();
    }

    void Update()
    {
        Enemy[] activeEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            // Check if the enemy is not already in the list
            if (!enemies.Contains(enemy))
            {
                // Add the new enemy to the list
                enemies.Add(enemy);

                // Do any other processing or handling for the new enemy here
            }
        }
        // Check for all the alphanumeric keys
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                char keyChar = (char)('a' + (i - (int)KeyCode.A));
                inputText += keyChar;
            }
        }

        // Check for spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputText += " ";
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (var enemy in enemies)
            {
                enemy.hitCheck(inputText);
            }
        }

        // Check for backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && inputText.Length > 0)
        {
            inputText = inputText.Substring(0, inputText.Length - 1);
        }
        
    }

    public void randomWordSelector()
    {
        string[] lines = csvFile.text.Split('\n'); // Split the CSV file into an array of lines.

        foreach (string line in lines)
        {
            string[] words = line.Split(','); // Split the line into words using a comma as the separator.

            foreach (string word in words)
            {
                string trimmedWord = word.Trim(); 

                if (!string.IsNullOrEmpty(trimmedWord))
                {
                    listOfTarget.Add(trimmedWord); 
                }
            }
        }
        for (int i = 0; i < listOfTarget.Count; i++)
        {
            //Debug.Log("Word " + i + ": " + listOfTarget[i]);
        }
    }
    
    public string GetRandomWordAndRemove()
    {
        if (listOfTarget.Count > 0)
        {
            int randomIndex = Random.Range(0, listOfTarget.Count); // Generate a random index.
            string randomWord = listOfTarget[randomIndex]; // Get the random word.

            listOfTarget.RemoveAt(randomIndex); // Remove the word from the list.

            return randomWord;
        }
        else
        {
            Debug.LogWarning("No more words in the list.");
            return null; // Return null or a message to indicate that the list is empty.
        }
    }
    
}
