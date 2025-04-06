using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSFXManager : MonoBehaviour
{
    public static AudioSFXManager Instance { get; private set; }

    public AudioSource audioSource;
    public AudioClip tap, click, pop, thump, zip, ding, bad, savePeople, deliverBelt, deliverNoBelt;

    private bool canPlay = true;
    private HashSet<string> gridScenes = new HashSet<string> { "Grid Day 1", "Grid Day 2", "Grid Day 3" };

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

        string scene = SceneManager.GetActiveScene().name;

        if (gridScenes.Contains(scene))
        {
            canPlay = false;
            StartCoroutine(EnableAudioAfterDelay(1f));
        }
    }

    public void PlayAudio(string clip)
    {
        if (!canPlay) return;

        switch (clip)
        {
            case "tap": audioSource.clip = tap; audioSource.volume = 0.5f; break;
            case "click": audioSource.clip = click; audioSource.volume = 1f; break;
            case "pop": audioSource.clip = pop; audioSource.volume = 1f; break;
            case "thump": audioSource.clip = thump; audioSource.volume = 1f; break;
            case "zip": audioSource.clip = zip; audioSource.volume = 1f; break;
            case "ding": audioSource.clip = ding; audioSource.volume = 1f; break;
            case "bad": audioSource.clip = bad; audioSource.volume = 1f; break;
            case "savePeople": audioSource.clip = savePeople; audioSource.volume = 1f; break;
            case "deliverBelt": audioSource.clip = deliverBelt; audioSource.volume = 0.5f; break;
            case "deliverNoBelt": audioSource.clip = deliverNoBelt; audioSource.volume = 0.5f; break;
            default: return;
        }

        audioSource.PlayOneShot(audioSource.clip);
    }

    private IEnumerator EnableAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlay = true;
    }
}
