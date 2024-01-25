using System;
using System.Collections.Generic;
using UnityEngine;

public enum Audios {
    AbilityCooldownEnd,
    AbilityShieldEnable,
    AbilityShieldBreak_1,
    AbilityShieldBreak_2,
    AbilityStarMario,
    AchievementNew,
    AmbientCriatureScary_1,
    AmbientSewer_1,
    AmbientSewer_2,
    AmbientSewer_3, 
    AmbientSewerWater_1,
    BatDie_1,
    BatIdle,
    BatIdle_1,
    BatIdle_2,
    EatQuesito_2,
    GameoverDie,
    GameplayMusic_1,
    GameplayMusic_2,
    MenuTap_1,
    RatIdle_1,
    RatIdle_2,
    RatJumpGoofy_1,
    RatMove_1,
    RatMove_2,
    RatMove_3,
    RatMove_4, 
    RatMove_5,
    RatMove_6,
    RatMove_7,
    RatRespawn,
    RatRespawn_1,
    RatSlideWater_1,
    RatSlideWater_2,
    RatSlideWater_3,
    RatSlideWater_4,
    RatWalkIdle_1,
    RatWalkIdle_2,
    RatWalkIdle_3,
    ShopBuy,
    StartCountDown,
    WallHit_1,
    WallHit_2,
    Music,
    BatPoisonball,
    BatPoisonball_1,
    Menu_Tab,
    ButtonClick_1,
    ButtonHover,
    RatShot,
    MovingBatAttack
}

public class SoundManager : MonoBehaviour
{
    private AudioSource effectManager;
    private AudioSource abilitiesManager;
    private AudioSource musicManager; 

    public static SoundManager Instance;
    public AudiosContainer audiosDatabase;

    private readonly Dictionary<Audios, AudioClip> soundsDatabase = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else Instance = this;
        effectManager = GetComponent<AudioSource>();
        abilitiesManager = transform.GetChild(0).GetComponentInChildren<AudioSource>();
        musicManager = transform.GetChild(1).GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        bool sucess;
        foreach (AudioClip clip in audiosDatabase.soundsDB)
        {
            if (sucess = Enum.TryParse(clip.name, out Audios value))
                soundsDatabase.Add(value, clip);
        }
    }
    public void PlayEnvironment(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value))
        {
            try
            {
                effectManager.PlayOneShot(value);
            }
            catch (ArgumentNullException)
            {

                //NO IDEA
            }
        
        }

    }
    public void PlayEffect(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value)) 
        {
            //if (clip == Audios.AbilityStarMario) musicManager.mute = true;
            abilitiesManager.PlayOneShot(value);
        }   
    }
}
