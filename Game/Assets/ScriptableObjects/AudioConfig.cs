using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum SoundType
{
    None,
    Extinguish,
    LightFire,
    Burn,
    Teleport
}

[CreateAssetMenu(fileName = "NewAudioConfig", menuName = "New AudioConfig")]
public class AudioConfig : ScriptableObject
{

    [SerializeField]
    private AudioSource sfxPlayerPrefab;
    public AudioSource SfxPlayerPrefab { get { return sfxPlayerPrefab; } }

    [SerializeField]
    private AudioSource musicPlayerPrefab;
    public AudioSource MusicPlayerPrefab { get { return musicPlayerPrefab; } }


    private AudioClip music;
    public AudioClip Music { get { return music; } }

    [SerializeField]
    private List<GameSound> sounds;

    public List<GameSound> Sounds { get { return sounds; } }

}


[System.Serializable]
public class GameSound : System.Object
{

    public SoundType soundType;
    public AudioClip sound;

    public List<AudioClip> sounds;
}