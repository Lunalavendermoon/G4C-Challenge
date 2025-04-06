using UnityEngine;

public class DialogueAssign : MonoBehaviour
{
    public Dialogue dialogueBeforeEnding;
    public Dialogue goodEnding;
    public Dialogue badEnding;
    public int threshold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.peopleFed >= threshold) {
            dialogueBeforeEnding.RootNode.nextDialogue = goodEnding;
        }
        else {
            dialogueBeforeEnding.RootNode.nextDialogue = badEnding;
        }
    }
}
