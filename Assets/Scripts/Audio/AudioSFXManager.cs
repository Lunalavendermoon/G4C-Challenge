using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    public static AudioSFXManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip tap;
    public AudioClip click;
    public AudioClip pop;
    public AudioClip thump;
    

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

    public void PlayAudio(string clip)
    {
        if (clip == "tap") {
            audioSource.clip = tap;
            audioSource.volume = 1f;
        }
        else if (clip == "click") {
            audioSource.clip = click;
            audioSource.volume = 1f;
        }
        else if (clip == "pop") {
            audioSource.clip = pop;
            audioSource.volume = 1f;
        }
        else if (clip == "thump") {
            audioSource.clip = thump;
            audioSource.volume = 0.5f;
        }
        audioSource.Play();
    }

}
