using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleAudioPlayer : MonoBehaviour
{
    private AudioSource audioSrc;

    void Start()
    {
        Debug.Log("SimpleAudioPlayer Start: Getting AudioSource...");
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null) {
            Debug.LogError("SimpleAudioPlayer Start: AudioSource NOT found!");
        } else {
            Debug.Log("SimpleAudioPlayer Start: AudioSource FOUND!");
        }
    }

    void Update()
    {
        // Check if the space bar is pressed down THIS FRAME
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SimpleAudioPlayer Update: Space Bar Pressed!");
            if (audioSrc != null)
            {
                Debug.Log("SimpleAudioPlayer Update: Attempting to play audio...");
                audioSrc.Play();
                Debug.Log("SimpleAudioPlayer Update: audioSrc.Play() called.");
            }
            else
            {
                Debug.LogWarning("SimpleAudioPlayer Update: Cannot play, audioSrc is null.");
            }
        }
    }
}