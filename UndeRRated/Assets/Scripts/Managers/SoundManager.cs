using System;
using System.Collections.Generic;
using UnityEngine;

public enum Audios {
    AbilityShieldEnable,
    AbilityShieldBreak_1,
    AbilityShieldStatic_1,
    AbilityShieldDisable_1,
    AbilityShieldBreak,
    AbilityShieldDisable,
    AbilityStar,
    AbilityStarMario_1,
    AchievementNew_1,
    AchievementNew_2,
    AchievementNew_3,
    AmbientCriatureScary_1,
    AmbientSewer_1,
    AmbientSewer_2,
    AmbientSewer_3, 
    AmbientSewerWater_1,
    BatDie,
    BatDie_1,
    BatIdle_1,
    BatIdle_2,
    EatQuesito_2,
    ButtonHover,
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
    AbilityShieldBreak_2,
    BatPoisonball,
    BatPoisonball_1,
    Menu_Tab,
    ButtonClick_1,
    ButtonClick_2,
    RatShot,
    MovingBatAttack
}

public class SoundManager : MonoBehaviour
{
    AudioSource effectManager;

    public static SoundManager Instance;

    public List<AudioClip> audios = new();  

    private readonly Dictionary<Audios, AudioClip> soundsDatabase = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(Instance);
        else Instance = this;
        effectManager = GetComponent<AudioSource>();      
        
    }

    private void Start()
    {
        bool sucess;
        foreach(AudioClip clip in audios)
        {
            if (sucess = Enum.TryParse(clip.name, out Audios value))
            {         
                soundsDatabase.Add(value, clip);
            }
            Debug.Assert(sucess, "Tried adding: " +clip.name);
        }
    }
    public void PlaySound(Audios clip)
    {
        if (soundsDatabase.TryGetValue(clip, out AudioClip value)) effectManager.PlayOneShot(value);  
    }
    public void StopSound() {
        effectManager.Stop();
    }
}
