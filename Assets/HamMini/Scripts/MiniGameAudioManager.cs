using UnityEngine;

public class MiniGameAudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // ��� ���� Ŭ��
    public AudioClip seedSound;       // Seed�� ����� �� ȿ����
    public AudioClip catSound;        // Cat�� ����� �� ȿ����
    public AudioClip gameClearSound;   // ���� Ŭ���� �� ȿ����

    public AudioSource backgroundAudioSource;  // ������ǿ� AudioSource
    public AudioSource effectsAudioSource;     // ȿ������ AudioSource

    void Start()
    {
        // AudioSource ������Ʈ ��������
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource = gameObject.AddComponent<AudioSource>();

        // ��� ���� ����
        PlayBackgroundMusic();
    }

    void PlayBackgroundMusic()
    {
        // ��������� ���ٸ� ������� ����
        if (backgroundMusic != null)
        {
            backgroundAudioSource.loop = true;  // ������� �ݺ� ���
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.Play();
        }
    }

    public void PlaySeedSound()
    {
        // Seed�� ����� �� ȿ���� ���
        if (seedSound != null)
        {
            effectsAudioSource.PlayOneShot(seedSound);  // ȿ���� ���
        }
    }

    public void PlayCatSound()
    {
        // Cat�� ����� �� ȿ���� ���
        if (catSound != null)
        {
            effectsAudioSource.PlayOneShot(catSound);  // ȿ���� ���
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
        // ������� ���߱�
        backgroundAudioSource.Stop();
    }
}
