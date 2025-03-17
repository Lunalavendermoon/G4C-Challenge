using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HelpButtonScript : MonoBehaviour
{
    public GameObject helpBg;
    public GameObject levelManager;

    BlockLevelManagerScript manager;

    void Start() {
        manager = levelManager.GetComponent<BlockLevelManagerScript>();
        hidePopup();
    }

    void OnMouseDown() {
        showPopup();
    }

    public void showPopup() {
        setStatus(true);
    }

    public void hidePopup() {
        setStatus(false);
    }

    void setStatus(bool status) {
        helpBg.SetActive(status);
        manager.setPopupStatus(status);
    }
}
