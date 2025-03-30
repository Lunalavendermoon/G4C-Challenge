using UnityEngine;

public class HelpButtonScript : MonoBehaviour
{
    public GameObject helpUiManager;
    HelpUiManager helpManager;
    private bool click = true;

    void Start() {
        helpManager = helpUiManager.GetComponent<HelpUiManager>();
    }

    void OnMouseDown() {
        if (click) {
            AudioSFXManager.Instance.PlayAudio("click");
            helpManager.showHelp();
            click = false;
        }
    }

    public void ButtonClickable (bool clickable) {
        click = clickable;
    }    
}
