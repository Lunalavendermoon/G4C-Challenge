using Unity.VisualScripting;
using UnityEngine;

public class Destination : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("win!");
        }
    }
}
