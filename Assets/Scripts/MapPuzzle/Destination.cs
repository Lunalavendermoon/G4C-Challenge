using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Destination : MonoBehaviour
{
    [SerializeField] GameObject belt1;
    [SerializeField] GameObject belt2;
    [SerializeField] Text warnText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (belt1.GetComponent<SpriteRenderer>().color == Color.red || belt2.GetComponent<SpriteRenderer>().color == Color.red)
            {
                warnText.text = "No, I can't leave the food in the food belt\nR to retry";
            }
            else
            {
                Debug.Log("win!");
            }
        }
    }
}
