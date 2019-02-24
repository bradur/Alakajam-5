
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
    private AudioSource secondSfxPlayer;

    private AudioSource musicPlayer;

    void Awake()
    {
        main = this;

    }

    private float timer = 0f;

    private void Start() {
        audioConfig = ConfigManager.main.GetConfig("AudioConfig") as AudioConfig;
        sfxPlayer = Instantiate(audioConfig.SfxPlayerPrefab);
        sfxPlayer.transform.SetParent(transform);
        secondSfxPlayer = Instantiate(audioConfig.SfxPlayerPrefab);
        secondSfxPlayer.transform.SetParent(transform);
        GameObject musicPlayerObject = GameObject.FindGameObjectWithTag("MusicPlayer");
        if (musicPlayerObject == null) {
            musicPlayer = Instantiate(audioConfig.MusicPlayerPrefab);
            musicPlayer.gameObject.tag = "MusicPlayer";
            musicPlayer.clip = audioConfig.Music;
            musicPlayer.volume = audioConfig.MusicVolume;
            DontDestroyOnLoad(musicPlayer);
        } else {
            musicPlayer = musicPlayerObject.GetComponent<AudioSource>();
        }
        musicMuted = audioConfig.MusicMuted;
        sfxMuted = audioConfig.MusicMuted;
        if (musicPlayer.clip != null)
        {
            if (musicMuted)
            {
                musicPlayer.Pause();
            }
            else if (!musicPlayer.isPlaying)
            {
                musicPlayer.Play();
            }
        }
    }

    IEnumerator FadeIn(AudioSource audioSource, AudioClip clip, float fadeTime, float targetVolume)
    {
        while (timer > 0) {
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }
        if (!audioSource.isPlaying) {
            audioSource.clip = clip;
            audioSource.Play();
        }
        float startVolume = audioSource.volume;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        
        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        audioSource.Pause();
    }

    public void FadeOutMusic() {
        if (musicPlayer.isPlaying) {
            StartCoroutine(FadeOut(musicPlayer, audioConfig.MusicFadeTime));
        }
    }

    public void FadeInBossMusic() {
        if (musicPlayer.clip != audioConfig.EndMusic) {
            StartCoroutine(FadeOut(musicPlayer, audioConfig.MusicFadeTime));
            timer = audioConfig.MusicFadeTime;
            StartCoroutine(FadeIn(musicPlayer, audioConfig.EndMusic, audioConfig.MusicFadeTime, audioConfig.MusicVolume));
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
                    AudioSource source = !sfxPlayer.isPlaying ? sfxPlayer : secondSfxPlayer;
                    source.clip = gameSound.sound;
                    if (gameSound.sounds.Count > 0)
                    {
                        source.clip = gameSound.sounds[Random.Range(0, gameSound.sounds.Count)];
                    }
                    source.Play();
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
                if (secondSfxPlayer.clip == gameSound.sound) {
                    DebugLogger.main.LogMessage("Clip {0} was stopped!", sfxPlayer.clip);
                    secondSfxPlayer.Stop();
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
