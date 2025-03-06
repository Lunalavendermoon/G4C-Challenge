using System;
using System.Collections.Generic;
 
[System.Serializable]
public class DialogueNode
{
    //public string text;
    public List<string> dialogues;
    public List<DialogueResponse> responses;
    
    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}