using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource musicSource; // Источник для музыки
    public AudioSource sfxSource; // Источник для звуковых эффектов

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f; // Громкость музыки
    [Range(0f, 1f)] public float sfxVolume = 1f; // Громкость звуковых эффектов

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликаты
        }
    }

    private void Start()
    {
        // Применяем сохраненные значения громкости
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolume();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }
}
