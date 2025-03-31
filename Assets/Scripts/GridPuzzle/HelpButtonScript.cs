using UnityEngine;

public class HelpButtonScript : MonoBehaviour
{
    public GameObject levelManager;
    private bool click = true;


    void OnMouseDown() {
        if (click) {
            AudioSFXManager.Instance.PlayAudio("ding");
            levelManager.GetComponent<BlockLevelManagerScript>().showHint();
            // click = false;
        }
    }

    public void ButtonClickable (bool clickable) {
        click = clickable;
    }
}
