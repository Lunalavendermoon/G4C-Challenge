using UnityEngine;

public class HunguryMan : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && MapLevelManager.Instance.getFood() >= 1)
        {
            playerMovement.giveFood(1);
            AudioSFXManager.Instance.PlayAudio("ding");
            MapLevelManager.Instance.setFood(1);
            Destroy(gameObject);
        }
    }
}
