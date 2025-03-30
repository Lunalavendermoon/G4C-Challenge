using UnityEngine;

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
    public string BGM;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (BGM != "") {
            PlayAudio(BGM);
        }
        audioSource.loop = true;
    }
    public void PlayAudio(string clip)
    {
        if (clip == "chill") {
            audioSource.clip = chill;
            audioSource.volume = 0.2f;
        }
        else if (clip == "sad") {
            audioSource.clip = sad;
            audioSource.volume = 1f;
        }
        else if (clip == "suspense") {
            audioSource.clip = suspense;
            audioSource.volume = 0.5f;
        }
        else if (clip == "story") {
            audioSource.clip = story;
            audioSource.volume = 0.5f;
        }
        else if (clip == "night") {
            audioSource.clip = night;
            audioSource.volume = 0.75f;
        }
        else if (clip == "grid") {
            audioSource.clip = grid;
            audioSource.volume = 0.5f;
        }
        audioSource.Play();
    }
}
