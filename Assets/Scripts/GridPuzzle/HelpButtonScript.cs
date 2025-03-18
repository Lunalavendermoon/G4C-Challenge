using UnityEngine;

public class HelpButtonScript : MonoBehaviour
{
    public GameObject helpUiManager;
    HelpUiManager helpManager;

    void Start() {
        helpManager = helpUiManager.GetComponent<HelpUiManager>();
    }

    void OnMouseDown() {
        helpManager.showHelp();
    }    
}
