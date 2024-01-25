using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioScriptableObject", menuName="Audios/Create Scriptable Audios")]
public class AudiosContainer : ScriptableObject
{
    public List<AudioClip> soundsDB = new();
}
