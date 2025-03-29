using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    public static AudioSFXManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip tap;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    

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
        else if (clip == "yay") {
            audioSource.clip = clip2;
            audioSource.volume = 1f;
        }
        else if (clip == "wee") {
            audioSource.clip = clip3;
            audioSource.volume = 1f;
        }
        else if (clip == "life") {
            audioSource.clip = clip4;
            audioSource.volume = 0.5f;
        }
        audioSource.Play();
    }

}
