using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapLevelManager : MonoBehaviour
{
    public static MapLevelManager Instance { get; private set; }

    public float foodOwn;
    [SerializeField] float beginFoodOwn;
    private float originFoodOwn;
    private Slider slider;
    private int sceneIndex;
    [SerializeField] float fillSpeed = 10f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        originFoodOwn = foodOwn;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        beginFoodOwn = foodOwn;
    }

    void Update()
    {
        slider.value = Mathf.MoveTowards(slider.value, foodOwn, 15f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            foodOwn = beginFoodOwn;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foodOwn = originFoodOwn;
            SceneManager.LoadScene(sceneIndex);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        if (foodOwn < 1)
        {
            slider.handleRect.gameObject.SetActive(false);
        }
        else
        {
            slider.handleRect.gameObject.SetActive(true);
        }

        if (slider == null)
        {
            Destroy(gameObject);
        }

        GameManager.foodGiven = originFoodOwn - foodOwn;

        Debug.Log(foodOwn);
    }

    public void setFood(float amount)
    {
        foodOwn -= amount;
    }

    public float getFood()
    {
        return foodOwn;
    }
}
