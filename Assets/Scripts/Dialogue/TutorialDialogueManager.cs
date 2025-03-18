using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TutorialDialogueManager : MonoBehaviour
{
    public static TutorialDialogueManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI DialogBodyText;
    public ChangeSprite background; // TODO delete and replace with canvas image
    public List<GameObject> disabledUI;

    [Header("Dialogue Data")]
    public Dialogue dialogue;
    private DialogueNode dialogueNode;
    private List<string> dialogues;

    private int dialogueCounter = 0;

    private bool dialogueIsActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple TutorialDialogueManager instances found!");
            Destroy(gameObject);
        }
        DOTween.Init();
    }

    private void Update()
    {
        if (!dialogueIsActive) {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !ItemDropLocation.mouseOverItemDropLocation)
        {
            if (dialogueCounter < dialogues.Count)
            {
                DialogueAssemble(dialogueCounter++);
            }
            else
            {
                HideDialogue();
            }
        }
    }

    public void StartDialogue()
    {
        dialogueIsActive = true;
        dialogueNode = dialogue.RootNode;
        dialogueCounter = 0;
        dialogues = new List<string>(dialogueNode.dialogues);
    }

    private void DialogueAssemble(int index)
    {
        string fullText = dialogues[index];

        DialogBodyText.text = AddTags(fullText);
    }

    public void HideDialogue()
    {
        dialogueIsActive = false;
        // TODO hide entire popup window
    }

    private string AddTags(string text) {
        string final = "";
        for (int i = 0; i < text.Length; ++i) {
            if (text[i] == '{') {
                final += "<color=#ffd666>";
            } else if (text[i] == '}') {
                final += "</color>";
            } else {
                final += text[i];
            }
        }
        return final;
    }

    private string GetName(string text)
    {
        int colonIndex = text.IndexOf(':');
        if (colonIndex == -1) return "";

        string name = text.Substring(0, colonIndex);
        return name switch
        {
            "y" => "You",
            "c" => "Clare",
            "p" => "Politician",
            "w" => "Worker",
            _ => name
        };
    }

    private string GetDialogue(string text)
    {
        int colonIndex = text.IndexOf(':');
        if (colonIndex == -1) return text.Substring(0, text.Length - 5).Trim();
        return text.Substring(colonIndex + 1, text.Length - colonIndex - 6).Trim();
    }

    private int GetImage1(string text) => ParseImageIndex(text, text.Length - 5);
    private int GetImage2(string text) => ParseImageIndex(text, text.Length - 3);

    private int ParseImageIndex(string text, int startIndex)
    {
        return int.Parse(text.Substring(startIndex, 2));
    }
}
