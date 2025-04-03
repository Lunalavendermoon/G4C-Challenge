using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBelt : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    private float foodHave;
    private float foodStored = 0;
    public bool haveStored = false;
    [SerializeField] GameObject cart;
    FoodBelt[] beltEnd;
    [System.Obsolete]

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            beltEnd = FindObjectsOfType<FoodBelt>();
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
                cart.GetComponent<Animator>().enabled = true;

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
                haveStored = true;
                foreach (FoodBelt copy in beltEnd)
                {
                    if (copy != this)
                    {
                        copy.foodStored = foodStored;
                        copy.haveStored = true;
                    }
                }
                Invoke("Stored", 2f);
            }
        }
    }

    void Stored()
    {
        foreach (FoodBelt copy in beltEnd)
        {
            if (copy != this)
            {
                copy.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        cart.GetComponent<Animator>().enabled = false;
    }
}
