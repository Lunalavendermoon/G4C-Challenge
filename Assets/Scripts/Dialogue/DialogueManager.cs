using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Threading; // Import DOTween

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject DialogueParent;
    public TextMeshProUGUI DialogTitleText, DialogBodyText;
    public ChangeSpriteUI image1, image2;
    public ChangeSprite background;
    public List<GameObject> disabledUI;

    [Header("Dialogue Data")]
    public Dialogue dialogue;
    private DialogueNode dialogueNode;
    private List<string> dialogues;

    private int dialogueCounter = 0;
    private bool responseDone = false;
    private int finishDialogue = 0; // 0 = not finished, 1 = finish immediately, -1 = finished, awaiting new dialogue

    private static Mutex mut = new Mutex();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple DialogueManager instances found!");
            Destroy(gameObject);
        }
        StartDialogue(dialogue.RootNode);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ItemDropLocation.mouseOverItemDropLocation)
        {
            mut.WaitOne();
            if (!responseDone && finishDialogue == 0)
            {
                finishDialogue = 1; // Skip to end of dialogue
                mut.ReleaseMutex();
            }
            else if (responseDone)
            {
                responseDone = false;
                mut.ReleaseMutex();
                if (dialogueCounter < dialogues.Count)
                {
                    DialogueAssemble(dialogueCounter++);
                }
                else if (dialogueNode.IsLastNode())
                {
                    HideDialogue();
                }
                else
                {
                    StartDialogue(dialogueNode.nextDialogue.RootNode);
                }
            }
        }
    }

    public void StartDialogue(DialogueNode node)
    {
        ShowDialogue();
        dialogueNode = node;
        dialogueCounter = 0;
        dialogues = new List<string>(node.dialogues);

        FadeTransition(() => DialogueAssemble(dialogueCounter++), node.bgNum);
    }

    private void DialogueAssemble(int index)
    {
        string fullText = dialogues[index];
        string title = GetName(fullText);
        string dialogue = GetDialogue(fullText);

        DialogTitleText.text = title;
        PrintWord(dialogue);
        image1.ChangeTo(GetImage1(fullText));
        image2.ChangeTo(GetImage2(fullText));

        // Apply GreyOut condition based on the last character
        GreyOutCharacter(fullText);
    }

    public void HideDialogue()
    {
        foreach (GameObject ui in disabledUI) ui.SetActive(true);
        DialogueParent.SetActive(false);

        FadeTransition(() => GameManager.LoadBlockScene(), 0);
    }

    private void ShowDialogue()
    {
        foreach (GameObject ui in disabledUI) ui.SetActive(false);
        DialogueParent.SetActive(true);
    }

    public bool IsDialogueActive()
    {
        return DialogueParent.activeSelf;
    }

    public async void PrintWord(string dialogue)
    {
        mut.WaitOne();
        responseDone = false;
        finishDialogue = 0;
        DialogBodyText.text = "";
        mut.ReleaseMutex();

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (finishDialogue == 1)
            {
                mut.WaitOne();
                DialogBodyText.text = dialogue;
                finishDialogue = -1;
                responseDone = true;
                mut.ReleaseMutex();
                return;
            }

            DialogBodyText.text += dialogue[i];
            await Task.Delay(20);
        }

        DialogBodyText.text = dialogue;
        responseDone = true;
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

    private void GreyOutCharacter(string fullText)
    {
        if (fullText.Length == 0) return;

        char lastChar = fullText[fullText.Length - 1];

        if (lastChar == 'l')
        {
            image2.GreyOut();
        }
        else if (lastChar == 'r')
        {
            image1.GreyOut();
        }
    }

    private void FadeTransition(Action onFadeComplete, int newBackgroundIndex)
    {
        float fadeDuration = 0.5f;

        // Get the SpriteRenderer for the background
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();

        // Fade out to black (Background uses SpriteRenderer, others use Image)
        bgRenderer.DOFade(0f, fadeDuration);
        image1.image.DOFade(0f, fadeDuration);
        image2.image.DOFade(0f, fadeDuration)
            .OnComplete(() =>
            {
                // Change Background and Sprites after fade out
                background.ChangeTo(newBackgroundIndex);
                onFadeComplete?.Invoke();

                // Fade back in
                bgRenderer.DOFade(1f, fadeDuration);
                image1.image.DOFade(1f, fadeDuration);
                image2.image.DOFade(1f, fadeDuration);
            });
    }

}
