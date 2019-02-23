
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public static AudioManager main;

    private AudioConfig audioConfig;

    private bool sfxMuted = false;

    private bool musicMuted = false;
    public bool MusicMuted { get { return musicMuted; } }

    private AudioSource sfxPlayer;

    private AudioSource musicPlayer;

    void Awake()
    {
        main = this;

    }

    private void Start() {
        audioConfig = ConfigManager.main.GetConfig("AudioConfig") as AudioConfig;
        sfxPlayer = Instantiate(audioConfig.SfxPlayerPrefab);
        sfxPlayer.transform.SetParent(transform);
        musicPlayer = Instantiate(audioConfig.MusicPlayerPrefab);
        musicPlayer.transform.SetParent(transform);
        musicPlayer.clip = audioConfig.Music;
        musicMuted = audioConfig.MusicMuted;
        sfxMuted = audioConfig.MusicMuted;
        if (musicPlayer.clip != null)
        {
            if (musicMuted)
            {
                musicPlayer.Pause();
            }
            else
            {
                musicPlayer.Play();
            }
        }
    }

    public void PlaySound(SoundType soundType)
    {
        if (!sfxMuted)
        {
            foreach (GameSound gameSound in audioConfig.Sounds)
            {
                if (gameSound.soundType == soundType)
                {
                    sfxPlayer.clip = gameSound.sound;
                    if (gameSound.sounds.Count > 0)
                    {
                        sfxPlayer.clip = gameSound.sounds[Random.Range(0, gameSound.sounds.Count)];
                    }
                    sfxPlayer.Play();
                }
            }
        }
    }
    public void StopSound(SoundType soundType)
    {
        foreach (GameSound gameSound in audioConfig.Sounds)
        {
            if (gameSound.soundType == soundType)
            {
                if (sfxPlayer.clip == gameSound.sound) {
                    DebugLogger.main.LogMessage("Clip {0} was stopped!", sfxPlayer.clip);
                    sfxPlayer.Stop();
                }
            }
        }
    }

    public void ToggleSfx()
    {
        sfxMuted = !sfxMuted;
    }

    public bool ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicMuted)
        {
            musicPlayer.Pause();
        }
        else
        {
            musicPlayer.Play();
        }
        return musicMuted;
    }
}
