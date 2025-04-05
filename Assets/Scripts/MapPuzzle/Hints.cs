using UnityEngine;

public class Hints : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] int whenToShow = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MapLevelManager.Instance.countRestart > whenToShow)
        {
            spriteRenderer.enabled = true;
        }
    }
}
