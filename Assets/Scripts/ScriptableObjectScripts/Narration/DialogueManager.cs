using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    private static Queue<string> lines;
    public static GameObject text;
    public static GameObject speaker;

    public static bool onConversation {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        
        text = GameObject.Find("DialogueText");
        speaker = GameObject.Find("SpeakerNameText");
        lines = new Queue<string>();

    }
    
    private void Start()
    {
    }

    private void Update()
    {
        if (onConversation && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentece();
        }
    }

    public static void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Start with" + dialogue.speakerName);
        if (lines == null)
        {
            Debug.Log("Fucke me in the arse daddyww");
        }
        lines.Clear();
        onConversation = true;
        
        speaker.GetComponent<TextMeshProUGUI>().text = dialogue.speakerName;

        foreach (string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextSentece();
    }

    public static void DisplayNextSentece()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = lines.Dequeue();
        
        Debug.Log(line);
        text.GetComponent<TextMeshProUGUI>().text = line;

    }

    static void  EndDialogue()
    {
        Debug.Log("End OF convo");
        onConversation = false;
    }
    
}
