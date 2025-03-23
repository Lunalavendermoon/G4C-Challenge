using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBelt : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] MapLevelManager mapLevelManager;
    private float foodHave;
    private float foodStored = 0;
    private bool haveStored = false;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foodHave = mapLevelManager.getFood();
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                playerMovement.giveFood(-foodStored);
                mapLevelManager.setFood(-foodStored);
                haveStored = false;
            }
            else if (!haveStored)
            {
                if (foodHave >= 2.5)
                {
                    foodStored = 2.5f;
                }
                else if (foodHave > 0 && foodHave < 2.5)
                {
                    foodStored = foodHave;
                }
                else{return;}

                playerMovement.giveFood(foodStored);
                mapLevelManager.setFood(foodStored);
                FoodBelt[] beltEnd = FindObjectsOfType<FoodBelt>();
                foreach (FoodBelt copy in beltEnd)
                {
                    if (copy != this)
                    {
                        copy.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        copy.foodStored = foodStored;
                        copy.haveStored = false;
                        haveStored = true;
                    }
                }
            }
        }
    }
}
