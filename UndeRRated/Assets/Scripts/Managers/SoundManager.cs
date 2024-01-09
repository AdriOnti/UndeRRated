using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource musicManager;
    AudioSource effectManager;

    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<string> audioNames = new List<string>();
    public static SoundManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);


        AudioSource[] soundManagers = GetComponentsInChildren<AudioSource>();

        if (soundManagers[0].name == "musicSource") musicManager = soundManagers[0]; 
        if (soundManagers[1].name == "effectsSource") effectManager = soundManagers[1];

    }

    private void Start()
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            audioNames.Add(audioClips[i].name);
        }
    }

    public void PlayEffect(string clipName)
    {
        effectManager.PlayOneShot(audioClips[audioNames.IndexOf(clipName)]);
    }
}
