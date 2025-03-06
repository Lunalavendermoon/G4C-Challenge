using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using System.Collections;
using System.Net.NetworkInformation;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject DialogueParent; // Main container for dialogue UI
    public TextMeshProUGUI DialogTitleText, DialogBodyText; // Text components for title and body
    public ChangeSpriteUI image1; // image on the left
    public ChangeSpriteUI image2; // image on the right
    public ChangeSprite background;
    public List<GameObject> disabledUI;
    private int currentItemNum = -1;
    private bool dialogueDone = true;
    private int dialogueCounter = 0;
    private int responseCounter = 0;
    private bool responseDone = false;
    private List<string> dialoguee;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple DialogueManager instances found!");
            Destroy(gameObject);
        }
 
        // Initially hide the dialogue UI
        HideDialogue();
    }

    public void Update() {

        if(!ItemDropLocation.mouseOverItemDropLocation && Input.GetMouseButtonDown(0) && responseDone == true)
        {
            if (dialogueCounter < dialoguee.Count){
                Dialoguetest(dialogueCounter);
                dialogueCounter++;
            }
            else if (dialogueCounter == dialoguee.Count && responseCounter == 0) {
                if (currentItemNum > -1){}
                HideDialogue();
            }
        }
    }
 
    // Starts the dialogue with given title and dialogue node
    public void StartDialogue(DialogueNode node)
    {
        // Display the dialogue UI
        ShowDialogue();
        dialogueCounter = 0;
 
        dialoguee = new List<string>(node.dialogues);
        responseCounter = node.responses.Count;
        Dialoguetest(0);
        dialogueCounter++;
    
        foreach (DialogueResponse response in node.responses)
        {
                DialogueManager.Instance.StartDialogue(response.Dialogue.RootNode);
        }
        
    }
    public void Dialoguetest(int index)
    {
        if (GetDialogue(dialoguee[index]) != DialogTitleText.text || index + 1 != dialoguee.Count) 
        {
            string title = GetName(dialoguee[index]);
            switch(title) 
            {
            case "m":
                title = "mom";
                break;
            case "d":
                title = "dad";
                break;
            default:
                break;
            }
            
            DialogTitleText.text = title;
            PrintWord(GetDialogue(dialoguee[index]));
            image1.Change(GetImage1(dialoguee[index]));
            image2.Change(GetImage2(dialoguee[index]));        
        }

    }
 
    // Hide the dialogue UI
    public void HideDialogue()
    {
        background.UnBlur();
        foreach(GameObject ui in disabledUI) {
            ui.SetActive(true);
        }
        DialogueParent.SetActive(false);
    }
 
    // Show the dialogue UI
    private void ShowDialogue()
    {
        background.ChangeToBlur();
        foreach(GameObject ui in disabledUI) {
            ui.SetActive(false);
        }
        DialogueParent.SetActive(true);
    }
 
    // Check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }
    
    //Checks the dialogue done boolean to see if the dialogue chain is completed.
    public bool GetDialogueDone() {
        return dialogueDone;
    }

    //shows dialogue letter-by-letter
    public async void PrintWord(string dialogue) {
        responseDone = false;
        DialogBodyText.text = "";
        foreach(char letter in dialogue)
        {
            DialogBodyText.text += letter;
            await Task.Delay(50);
        }
        DialogBodyText.text = dialogue;
        responseDone = true;
    }

    private string GetName(string text) {
        string name = "";
        int i = 0;
        if (text.IndexOf(':') == -1) {
            return "";
        }
        while(text[i] != ':' && i < text.Length - 2) {
            name = name + text[i];
            i++;
        }
        return name;
    }

    private string GetDialogue(string text) {
        string dialogue = "";
        if (GetName(text) == "") {
            for (int j = 0; j < text.Length - 2; j++) {
                dialogue = dialogue + text[j];
            }
        }
        else {
            int i = 0;
            while(text[i] != ':' && i < text.Length - 1) {
                i++;
            }
            for (int j = i + 1; j < text.Length - 2; j++) {
                dialogue = dialogue + text[j];
            }
        }
        return dialogue;
    }

    private char GetImage1(string text) {
        return text[text.Length - 2];
    }

    private char GetImage2(string text) {
        return text[text.Length - 1];
    }
}
    