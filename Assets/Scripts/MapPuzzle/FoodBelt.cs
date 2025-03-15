using System.Collections.Generic;
using UnityEngine;

public class FoodBelt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                FoodBelt[] beltEnd = FindObjectsOfType<FoodBelt>();
                foreach (FoodBelt copy in beltEnd)
                {
                    if (copy != this)
                    {
                        copy.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                    }
                }
            }
        }
    }
}
