using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] float moveDistance = 0.5f;
    private float foodGiven = 4;
    private int status = 1;
    private bool canMove = true;
    [SerializeField] bool isRiding = false;
    private Vector2 railRoadDirection = Vector2.up;
    [SerializeField] int step = 29;
    [SerializeField] int foodOwn = 4;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            moveeee(new Vector3(0f, moveDistance, 0f));
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMove)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            moveeee(new Vector3(-moveDistance, 0f, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            moveeee(new Vector3(0f, -moveDistance, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            moveeee(new Vector3(moveDistance, 0f, 0f));
        }

        if (foodGiven < 2.5)
        {
            status = 1;
            transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        }
        else if (foodGiven > 2.5 && foodGiven < 5)
        {
            status = 2;
            transform.localScale = new Vector3(0.3f, 0.3f, 0f);
        }
        else if (foodGiven >= 5)
        {
            status = 3;
            transform.localScale = new Vector3(0.2f, 0.2f, 0f);
        }

        if (isRiding)
        {
            transform.Translate(railRoadDirection.normalized * 5f * Time.deltaTime, Space.Self);
            canMove = false;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            foodGiven += 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foodGiven -= 1;
        }

        if (step == 0)
        {
            canMove = false;
        }
    }


    private void moveeee(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D bigGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("BigGround"));
        Collider2D middleGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("MiddleGround"));
        Collider2D smallGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("SmallGround"));
        Collider2D railRoad = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("RailRoad"));

        if (railRoad != null)
        {
            isRiding = true;
            return;
        }

        if (status == 3)
        {
            if (bigGround == null && middleGround == null && smallGround == null)
            {
                return;
            }
        }
        if (status == 2)
        {
            if (bigGround == null && middleGround == null)
            {
                return;
            }
        }
        if (status == 1)
        {
            if (bigGround == null)
            {
                return;
            }
        }
        step -= 1;
        StartCoroutine(leap(destination));
    }


    private IEnumerator leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position;

        float elapsed = 0f;
        float duration = 0.12f;

        while (elapsed < duration)
        {
            float time = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, time);
            elapsed += Time.deltaTime;
            canMove = false;
            yield return null;
        }

        transform.position = destination;
        canMove = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RailEnds") && isRiding)
        {
            transform.position = collision.transform.position;
            isRiding = false;
            canMove = true;
        }
    }

    public void giveFood()
    {
        foodGiven += 1;
    }

    public int getFood()
    {
        return foodOwn;
    }
}
