using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Option,
        Eat,
        Jump,
        Pick,
        Drop
    }

    public static AudioManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("AudioClip")]
    private Dictionary<SoundType, AudioClip> soundClips;

    public AudioClip pencilSound;
    public AudioClip eatingSound;
    public AudioClip jumpSound;
    public AudioClip pickItemSound;
    public AudioClip dropItemSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
            SceneManager.sceneLoaded += OnSceneLoaded; // �� ���� �̺�Ʈ ���

        }
        else
        {
            Destroy(gameObject); // �ߺ� ���� ����
        }

        soundClips = new Dictionary<SoundType, AudioClip>
        {
            { SoundType.Option, pencilSound },
            { SoundType.Eat, eatingSound },
            { SoundType.Jump, jumpSound },
            { SoundType.Pick, pickItemSound },
            { SoundType.Drop, dropItemSound }
        };
    }

    private void Start()
    {
        FindAudioElements();
        LoadVolumeSettings();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAudioElements(); // �� ������ ����� UI ã�Ƽ� ����
        LoadVolumeSettings(); // ���� ����� ������ ����� ������ ����
    }

    private void FindAudioElements()
    {
        bgmAudioSource = GameObjectUtils.FindInActiveObjectByName("BGM Audio Source")?.GetComponent<AudioSource>();
        sfxAudioSource = GameObjectUtils.FindInActiveObjectByName("SFX Audio Source")?.GetComponent<AudioSource>();

        SetAudioSource();

        bgmSlider = GameObjectUtils.FindInActiveObjectByName("BGM_Slider")?.GetComponent<Slider>();
        sfxSlider = GameObjectUtils.FindInActiveObjectByName("SFX_Slider")?.GetComponent<Slider>();

        // �����̴� �̺�Ʈ �����ʸ� �ٽ� ����Ͽ� ���� ���� �����ϵ��� ����
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveAllListeners(); // ���� �̺�Ʈ ����
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f); // ����� ���� �� ����
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners(); // ���� �̺�Ʈ ����
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f); // ����� ���� �� ����
        }
    }

    private void LoadVolumeSettings()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (bgmAudioSource != null) bgmAudioSource.volume = bgmVolume;
        if (sfxAudioSource != null) sfxAudioSource.volume = sfxVolume;

        if (bgmSlider != null)
        {
            bgmSlider.value = bgmVolume;
            bgmSlider.onValueChanged.AddListener(SetBGMVolume); // �����̴� �̺�Ʈ �߰�
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume); // �����̴� �̺�Ʈ �߰�
        }
    }

    #region Pre Setting
    public void SetBGMVolume(float volume)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = volume;
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);

            // ȿ���� �̸� ���: �̹� ��� ���̸� �ߴ��ϰ�, �̸� ��� �ڷ�ƾ ����
            sfxAudioSource.Stop();
            StartCoroutine(PreviewSFX());
        }
    }

    private IEnumerator PreviewSFX()
    {
        // �̸� ��⸦ ���� Ŭ���� sfxAudioSource�� �̹� �Ҵ�Ǿ� �־�� �մϴ�.
        if (sfxAudioSource.clip != null)
        {
            sfxAudioSource.time = 0f;
            PlaySound(SoundType.Option);
            // �Ҹ����� �г��� �����ִٸ� istime = 2 f �г��� �����ִٸ� time =0 
            yield return new WaitForSeconds(2f); // 2�� ���� ���
            sfxAudioSource.Stop();
        }
    }

    private void SetAudioSource()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.loop = true;
            if (!bgmAudioSource.isPlaying) // �ߺ� ���� ����
            {
                bgmAudioSource.Play();
            }
        }

        if (sfxAudioSource != null)
        {
            sfxAudioSource.playOnAwake = false;
            sfxAudioSource.loop = false;
        }
    }
    #endregion

    public void PlaySound(SoundType soundType)
    {
        if (soundClips.TryGetValue(soundType, out AudioClip clip) && clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
}
