using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour
{
    [SerializeField] GameObject belt1;
    [SerializeField] GameObject belt2;
    [SerializeField] Text warnText;
    [SerializeField] bool isLastPuzzle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (belt1 == null &&  belt2 == null)
            {
                if (isLastPuzzle)
                {
                    Destroy(MapLevelManager.Instance.gameObject);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (belt1.GetComponent<SpriteRenderer>().color == Color.red || belt2.GetComponent<SpriteRenderer>().color == Color.red)
            {
                warnText.text = "There's still food left in the transport cart!\nR to retry";
            }
            else
            {
                if (isLastPuzzle)
                {
                    Destroy(MapLevelManager.Instance.gameObject);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
