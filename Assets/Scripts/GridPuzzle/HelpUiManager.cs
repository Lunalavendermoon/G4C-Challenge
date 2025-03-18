using UnityEngine;

public class HelpUiManager : MonoBehaviour
{
    public GameObject blockLevelManager;
    public GameObject tutorialDialogueManager;
    public GameObject tutorialContent;
    public GameObject helpBg;
    public GameObject helpChoice;

    TutorialDialogueManager dialogueManager;

    BlockLevelManagerScript levelManager;

    void Start() {
        levelManager = blockLevelManager.GetComponent<BlockLevelManagerScript>();
        dialogueManager = tutorialDialogueManager.GetComponent<TutorialDialogueManager>();
        hideHelp();
    }

    public void startTutorialDay1() {
        // TODO show UI and tutorial, hide UI after
    }

    public void startTutorialFromButton() {
        // TODO show tutorial and hide UI after
    }

    public void showHelp() {
        setStatus(true);
        helpChoice.SetActive(true);
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
