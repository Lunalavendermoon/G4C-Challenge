using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.peopleFedlv = 0;
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Puzzle1_1" || scene == "Puzzle2_1" || scene == "Puzzle3_1") {
            GameManager.peopleFed1stlv = 0;
        }
    }
}
