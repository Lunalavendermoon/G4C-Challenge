using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBelt : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    private float foodHave;
    private float foodStored = 0;
    public bool haveStored = false;

    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FoodBelt[] beltEnd = FindObjectsOfType<FoodBelt>();
            foodHave = MapLevelManager.Instance.getFood();
            if (gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                playerMovement.giveFood(-foodStored);
                MapLevelManager.Instance.setFood(-foodStored);
                haveStored = false;
                foreach (FoodBelt copy in beltEnd)
                {
                    if (copy != this)
                    {
                        copy.haveStored = false;
                    }
                }
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
                MapLevelManager.Instance.setFood(foodStored);
                foreach (FoodBelt copy in beltEnd)
                {
                    if (copy != this)
                    {
                        copy.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        copy.foodStored = foodStored;
                        haveStored = true;
                    }
                }
            }
        }
    }
}
