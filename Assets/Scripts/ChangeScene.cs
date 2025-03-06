using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public int startOfGame = 0;
    public void NextScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
