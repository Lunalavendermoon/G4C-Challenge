using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveDistance = 0.5f;
    [SerializeField] int step = 29;
    private bool canMove = true;

    private float railSpd = 5f;
    [SerializeField] bool isRiding = false;
    private Vector2 railRoadDirection = Vector2.up;

    public float foodGiven = 0;
    private int status = 1;




    [SerializeField] Text setpText;
    [SerializeField] Text noStep;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6 && MapLevelManager.Instance.foodOwn != 3.5)
        {
            foodGiven--;
        }
        if (SceneManager.GetActiveScene().buildIndex == 11 && MapLevelManager.Instance.foodOwn != 4)
        {
            foodGiven--;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            moveeee(new Vector3(0f, moveDistance, 0f));
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
            transform.localScale = new Vector3(0.7f, 0.7f, 0f);
        }
        else if (foodGiven >= 2.5 && foodGiven < 5)
        {
            status = 2;
            transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        }
        else if (foodGiven >= 5)
        {
            status = 3;
            transform.localScale = new Vector3(0.3f, 0.3f, 0f);
        }

        if (isRiding)
        {
            transform.Translate(railRoadDirection.normalized * railSpd * Time.deltaTime, Space.Self);
            railSpd += 30f * Time.deltaTime;
            canMove = false;
        }
        else
        {
            railSpd = 5f;
        }

        if (step <= 0)
        {
            canMove = false;
            noStep.text = "Ran out of steps\n'R' to retry";
        }

        setpText.text = step.ToString();
    }


    private void moveeee(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        AudioSFXManager.Instance.PlayAudio("tap");
        Collider2D bigGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("BigGround"));
        Collider2D middleGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("MiddleGround"));
        Collider2D smallGround = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("SmallGround"));
        Collider2D railRoad = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("RailRoad"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));

        if (barrier != null)
        {
            Debug.Log("can't move");
            return;
        }

        if (railRoad != null && (Quaternion.Angle(transform.rotation, railRoad.transform.rotation) < 10 || (Quaternion.Angle(transform.rotation, railRoad.transform.rotation) > 80 && Quaternion.Angle(transform.rotation, railRoad.transform.rotation) < 100)))
        {
            isRiding = true;
            AudioSFXManager.Instance.PlayAudio("zip");
            step -= 1;
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
        step -= 1;
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

    public void giveFood(float amount)
    {
        foodGiven += amount;
        //AudioSFXManager.Instance.PlayAudio("pop");
    }
}
