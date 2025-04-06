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
            bool beltsNull = belt1 == null && belt2 == null;
            bool beltsSafe = !beltsNull && 
                             belt1.GetComponent<SpriteRenderer>().color != Color.red && 
                             belt2.GetComponent<SpriteRenderer>().color != Color.red;

            if (beltsNull || beltsSafe)
            {
                if (isLastPuzzle)
                {
                    GameManager.peopleFed += MapLevelManager.Instance.GetpeopleWFood();
                    MapLevelManager.Instance.peopleWFoodReset();
                    Destroy(MapLevelManager.Instance.gameObject);
                }

                MapLevelManager.Instance.countRestart = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                warnText.text = "There's still food left in the transport cart!\nR to retry";
            }
        }
    }
}
