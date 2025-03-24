using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TutorialDialogueManager : MonoBehaviour
{
    public static TutorialDialogueManager Instance { get; private set; }

    public GameObject helpUiManager;

    [Header("UI Elements")]
    public TextMeshProUGUI DialogBodyText;
    public RawImage background;

    public List<Texture> tutorialBackgrounds;

    [Header("Dialogue Data")]
    public Dialogue dialogue;
    private DialogueNode dialogueNode;
    private List<string> dialogues;

    private int dialogueCounter = 0;

    private bool dialogueIsActive = false;
    private float timermax = 0.1f;
    private float timer = 0.0f;

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
        if (timer > 0.0f) {
            timer -= Time.deltaTime;
            return;
        }
        if (Input.GetMouseButtonDown(0) && !ItemDropLocation.mouseOverItemDropLocation)
        {
            timer = timermax;
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
        
        DialogueAssemble(dialogueCounter++);
    }

    private void DialogueAssemble(int index)
    {
        string fullText = dialogues[index];

        DialogBodyText.text = AddTags(fullText);

        background.texture = tutorialBackgrounds[index];
    }

    public void HideDialogue()
    {
        dialogueIsActive = false;
        helpUiManager.GetComponent<HelpUiManager>().hideHelpFromTutorial();
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
}
