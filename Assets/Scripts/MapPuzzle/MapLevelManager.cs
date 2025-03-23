using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MapLevelManager : MonoBehaviour
{
    public static float foodOwn = 4;
    [SerializeField] Slider slider;  // 拖拽 Slider 进来
    [SerializeField] float fillSpeed = 10f;


    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = foodOwn;
    }

    void Update()
    {
        slider.value = Mathf.MoveTowards(slider.value, foodOwn, 15f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (foodOwn < 1)
        {
            slider.handleRect.gameObject.SetActive(false);
        }
        else
        {
            slider.handleRect.gameObject.SetActive(true);
        }
    }

    // 调用这个方法更新进度
    //public void SetProgress(float progress)
    //{
    //    foodOwn = Mathf.Clamp(progress, slider.minValue, slider.maxValue);
    //}

    public void setFood(float amount)
    {
        foodOwn -= amount;
    }

    public float getFood()
    {
        return foodOwn;
    }
}
