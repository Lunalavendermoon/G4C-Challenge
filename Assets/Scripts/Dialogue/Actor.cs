using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public Dialogue Dialogue;
    public int num; // item index
    private bool first = true;

    //starting automatic story dialogues
    void Update() {
    }
 
    //starting item dialogues in point-and-click section
    void OnMouseDown() 
    {
        if(!ItemDropLocation.mouseOverItemDropLocation) {SpeakTo();}
    }
    // Trigger dialogue for this actor
    public void SpeakTo()
    {
        
        if (DialogueManager.Instance != null && Dialogue != null && Dialogue.RootNode != null)
        {
            if (!DialogueManager.Instance.IsDialogueActive() && first) {
                
                DialogueManager.Instance.StartDialogue(Dialogue.RootNode);
                if(gameObject.GetComponent<Button>() == null){
                    first = false; 
                    gameObject.SetActive(false);
                }
            }
            Debug.Log("success");
        }
        else
        {
            if (DialogueManager.Instance == null)
            {
                Debug.LogError("DialogueManager.Instance is null!");
                return;
            }

            if (Dialogue == null)
            {
                Debug.LogError($"Actor '{gameObject.name}' does not have a Dialogue assigned!");
                return;
            }

            if (Dialogue.RootNode == null)
            {
                Debug.LogError($"Dialogue '{Dialogue.name}' does not have a RootNode assigned!");
                return;
            }

            //Debug.Log($"Actor '{gameObject.name}' starting dialogue with RootNode: {Dialogue.RootNode.name}");

        }
    }
}