using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<Sound> sounds;

    private Dictionary<Sound.SoundName, AudioSource> _audioSources = new Dictionary<Sound.SoundName, AudioSource>();

    private void Awake()
    {
        if (Instance != null) return;

        DontDestroyOnLoad(gameObject);
        Instance = this;
        foreach (var sound in sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;

            _audioSources[sound.soundName] = source;
        }

        PlaySound(Sound.SoundName.PlayOnLoad);
    }

    public void PlaySound(Sound.SoundName soundName)
    {
        if (_audioSources.ContainsKey(soundName))
        {
            if (_audioSources[soundName].loop && _audioSources[soundName].isPlaying) return;
            _audioSources[soundName].Play();
        }
    }

    public void PauseSound(Sound.SoundName soundName)
    {
        if (_audioSources.ContainsKey(soundName))
        {
            _audioSources[soundName].Pause();
        }

    }

    [System.Serializable]
    public class Sound
    {
        public enum SoundName
        {
            PlayOnLoad, ScoreIncreased, Ripple, EnemyHitPlayer, GameOver, Play,
            PlayerSaved, WaterRising, WaterFalling, PlayerHitObstacle
        }

        public SoundName soundName;
        public AudioClip clip;
        [Range(0, 1)] public float volume;
        [Range(0.1f, 3)] public float pitch;
        public bool loop;
    }
}
