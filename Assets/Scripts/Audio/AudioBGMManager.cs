using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioBGMManager : MonoBehaviour
{
    public static AudioBGMManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip chill;
    public AudioClip sad;
    public AudioClip suspense;
    public AudioClip story;
    public AudioClip night;
    public AudioClip grid;
    public AudioClip map;
    private string currentBGM = "";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.loop = true;
    }

    private void Update()
    {
        if (currentBGM != "map")
        {
            switch(SceneManager.GetActiveScene().name) {
                case "Puzzle1_1":
                    PlayAudio("map");
                    break;
                case "Puzzle2_1":
                    PlayAudio("map");
                    break;
                case "Puzzle3_1":
                    PlayAudio("map");
                    break;
                default:
                    break;
            }
        }
        if (currentBGM != "grid")
        {
            switch(SceneManager.GetActiveScene().name) {
                case "Grid Day 1":
                    PlayAudio("grid");
                    break;
                case "Grid Day 2":
                    PlayAudio("grid");
                    break;
                case "Grid Day 3":
                    PlayAudio("grid");
                    break;
                default:
                    break;
            }
        }
    }
    public void PlayAudio(string clip)
    {
        if (clip == "chill") {
            audioSource.clip = chill;
            audioSource.volume = 0.1f;
        }
        else if (clip == "sad") {
            audioSource.clip = sad;
            audioSource.volume = 0.4f;
        }
        else if (clip == "suspense") {
            audioSource.clip = suspense;
            audioSource.volume = 0.2f;
        }
        else if (clip == "story") {
            audioSource.clip = story;
            audioSource.volume = 0.3f;
        }
        else if (clip == "night") {
            audioSource.clip = night;
            audioSource.volume = 0.4f;
        }
        else if (clip == "grid") {
            audioSource.clip = grid;
            audioSource.volume = 0.5f;
            currentBGM = "grid";
        }
        else if (clip == "map") {
            audioSource.clip = map;
            audioSource.volume = 1f;
            currentBGM = "map";
        }
        audioSource.Play();
    }
}
