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
    public GameObject helpbuttonscript;

    [Header("UI Elements")]
    public TextMeshProUGUI DialogBodyText;
    public RawImage background;

    public List<Texture> tutorialBackgrounds;
    public Texture tutorialYapBg;

    [Header("Dialogue Data")]
    public Dialogue dialogueDay1;
    public Dialogue dialogueDay3;
    public Dialogue dialogueTutorial;
    private int day = 0;
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
        if (timer > 0.0f) {
            timer -= Time.deltaTime;
            return;
        }
        if (!dialogueIsActive) {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !ItemDropLocation.mouseOverItemDropLocation)
        {
            if (dialogueCounter < dialogues.Count)
            {
                AudioSFXManager.Instance.PlayAudio("tap");
                DialogueAssemble(dialogueCounter++);
            }
            else if (dialogueNode.IsLastNode() || day == 3)
            {
                helpbuttonscript.GetComponent<HelpButtonScript>().ButtonClickable(true);
                HideDialogue();
            }
            else
            {
                StartDialogueFromTutorial();
            }
        }
    }

    public void StartDialogueFromDay(int day)
    {
        dialogueIsActive = true;
        dialogueNode = day == 1 ? dialogueDay1.RootNode : dialogueDay3.RootNode;
        dialogueCounter = 0;
        dialogues = new List<string>(dialogueNode.dialogues);

        this.day = day;
        
        DialogueAssemble(dialogueCounter++);
    }

    public void StartDialogueFromTutorial() {
        dialogueIsActive = true;
        dialogueNode = dialogueTutorial.RootNode;
        dialogueCounter = 0;
        dialogues = new List<string>(dialogueNode.dialogues);

        day = 0;
        
        DialogueAssemble(dialogueCounter++);
    }

    private void DialogueAssemble(int index)
    {
        timer = timermax;
        string fullText = dialogues[index];

        DialogBodyText.text = AddTags(fullText);

        background.texture = (day == 1 || day == 3) ? tutorialYapBg : tutorialBackgrounds[index];
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
