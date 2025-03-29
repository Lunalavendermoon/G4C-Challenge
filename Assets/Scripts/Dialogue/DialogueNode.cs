using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    //public string text;
    [TextArea(3,20)]
    public List<string> dialogues;
    public Dialogue nextDialogue;
    public int bgNum;
    public string bgm;
            
    internal bool IsLastNode()
    {
        bool isLast = ReferenceEquals(nextDialogue, null);
        Debug.Log("last node: " + isLast);
        return isLast;
    }

}