using UnityEngine;

public class MiniGameAudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // 배경 음악 클립
    public AudioClip seedSound;       // Seed와 닿았을 때 효과음
    public AudioClip catSound;        // Cat과 닿았을 때 효과음
    public AudioClip gameClearSound;   // 게임 클리어 시 효과음

    public AudioSource backgroundAudioSource;  // 배경음악용 AudioSource
    public AudioSource effectsAudioSource;     // 효과음용 AudioSource

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource = gameObject.AddComponent<AudioSource>();

        // 배경 음악 시작
        PlayBackgroundMusic();
    }

    void PlayBackgroundMusic()
    {
        // 배경음악이 없다면 재생하지 않음
        if (backgroundMusic != null)
        {
            backgroundAudioSource.loop = true;  // 배경음악 반복 재생
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.Play();
        }
    }

    public void PlaySeedSound()
    {
        // Seed와 닿았을 때 효과음 재생
        if (seedSound != null)
        {
            effectsAudioSource.PlayOneShot(seedSound);  // 효과음 재생
        }
    }

    public void PlayCatSound()
    {
        // Cat과 닿았을 때 효과음 재생
        if (catSound != null)
        {
            effectsAudioSource.PlayOneShot(catSound);  // 효과음 재생
        }
        StopBackgroundMusic();
    }


    public void PlayGameClearSound()
    {
        if (gameClearSound != null)
        {
            effectsAudioSource.PlayOneShot(gameClearSound);
        }
        StopBackgroundMusic();
    }

    public void StopBackgroundMusic()
    {
        // 배경음악 멈추기
        backgroundAudioSource.Stop();
    }
}
