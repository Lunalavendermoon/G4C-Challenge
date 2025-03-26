using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapLevelManager : MonoBehaviour
{
    public static MapLevelManager Instance { get; private set; }
    public static float foodGiven;

    public float foodOwn;
    private float beginFoodOwn;
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
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
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

        foodGiven = originFoodOwn - foodOwn;
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
