using UnityEngine;

public class HelpUiManager : MonoBehaviour
{
    public GameObject blockLevelManager;
    public GameObject tutorialDialogueManager;
    public GameObject tutorialContent;
    public GameObject helpBg;
    public GameObject helpButton;
    public GameObject closeButton;

    TutorialDialogueManager dialogueManager;

    BlockLevelManagerScript levelManager;

    void Start() {
        levelManager = blockLevelManager.GetComponent<BlockLevelManagerScript>();
        dialogueManager = tutorialDialogueManager.GetComponent<TutorialDialogueManager>();
        hideHelp();
    }

    public void startTutorialDay1() {
        setStatus(true);
        closeButton.SetActive(false);
        startTutorialFromButton();
    }

    public void startTutorialFromButton() {
        // helpChoice.SetActive(false);
        tutorialContent.SetActive(true);
        levelManager.setPopupStatus(true);
        helpButton.GetComponent<HelpButtonScript>().ButtonClickable(false);

        dialogueManager.StartDialogue();
    }

    public void hideHelpFromTutorial() {
        closeButton.SetActive(true);
        hideHelp();
    }

    public void showHelp() {
        setStatus(true);
        // helpChoice.SetActive(true);
        tutorialContent.SetActive(false);
    }

    public void hideHelp() {
        setStatus(false);
    }

    void setStatus(bool status) {
        helpBg.SetActive(status);
        levelManager.setPopupStatus(status);
    }
}
