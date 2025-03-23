using UnityEngine;

public class HunguryMan : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] MapLevelManager mapLevelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && mapLevelManager.getFood() >= 1)
        {
            playerMovement.giveFood(1);
            mapLevelManager.setFood(1);
            Destroy(gameObject);
        }
    }
}
