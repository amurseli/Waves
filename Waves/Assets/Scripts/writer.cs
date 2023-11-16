using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class writer : MonoBehaviour
{
    private string inputText = "";
    
    private List<string> listOfTarget = new List<string>();
    public List<Enemy> enemies = new List<Enemy>();

    public TextMeshProUGUI canvasField;

    public TextAsset csvFile;
    private int currentIndex = 0; // Track the current index in the randomized list

    private void Awake()
    {
        randomWordSelector();
    }

    void Update()
    {
        Enemy[] activeEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in activeEnemies)
        {
            if (!enemies.Contains(enemy))
            {
                // Add the new enemy to the list
                enemies.Add(enemy);

                // Assign a random word to the new enemy
                AssignRandomWordToEnemy(enemy);

                // Do any other processing or handling for the new enemy here
            }
        }

        if (inputText.Length <= 30)
        {
            // Check for all the alphanumeric keys
            for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    char keyChar = (char)('a' + (i - (int)KeyCode.A)); 
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        keyChar = char.ToUpper(keyChar);
                    }
                    inputText += keyChar;
                }
            }

            if (Input.inputString.Contains("ñ"))
            {
                inputText += "ñ";
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputText += " ";
            }
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (var enemy in enemies)
            {
                bool kill = false;
                kill = enemy.hitCheck(inputText);

            }
            inputText = "";

        }

        // Check for backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && inputText.Length > 0)
        {
            inputText = inputText.Substring(0, inputText.Length - 1);
        }
        
        canvasField.text = inputText;
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
        int n = listOfTarget.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (listOfTarget[k], listOfTarget[n]) = (listOfTarget[n], listOfTarget[k]);
        }
    }
    

    public string GetNextWordAndRemove()
    {
        bool reuseWord = false;
        if (currentIndex > 3)
        {   //Logica de reusar palabras para generar enemigos con palabras iguales
            reuseWord = Random.Range(0, 100) < 5;
        }
        
        if (currentIndex < listOfTarget.Count && !reuseWord)
        {
            string nextWord = listOfTarget[currentIndex];
            currentIndex++; // Move to the next index
            return nextWord;
        }else if (reuseWord)
        {
            string nextWord = listOfTarget[currentIndex - Random.Range(1,3)];
            Debug.Log("repeted Word " + nextWord);
            return nextWord;
        }
        
        Debug.LogWarning("No more words in the list.");
        return "NoWorldLeft"; // Return null or a message to indicate that the list is empty.

    }
    
    public void AssignRandomWordToEnemy(Enemy enemy)
    {
        string randomWord = GetNextWordAndRemove();

        if (randomWord != null)
        {
            enemy.AssignWord(randomWord);
        }
    }
    
    int CountCharacters(string inputString)
    {
        return inputString.Length;
    }
    
    public void RemoveEnemyFromList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
    
}
