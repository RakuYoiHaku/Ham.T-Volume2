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
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 변경 이벤트 등록

        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
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
        FindAudioElements(); // 새 씬에서 오디오 UI 찾아서 연결
        LoadVolumeSettings(); // 씬이 변경될 때마다 저장된 볼륨을 적용
    }

    private void FindAudioElements()
    {
        bgmAudioSource = GameObjectUtils.FindInActiveObjectByName("BGM Audio Source")?.GetComponent<AudioSource>();
        sfxAudioSource = GameObjectUtils.FindInActiveObjectByName("SFX Audio Source")?.GetComponent<AudioSource>();

        SetAudioSource();

        bgmSlider = GameObjectUtils.FindInActiveObjectByName("BGM_Slider")?.GetComponent<Slider>();
        sfxSlider = GameObjectUtils.FindInActiveObjectByName("SFX_Slider")?.GetComponent<Slider>();

        // 슬라이더 이벤트 리스너를 다시 등록하여 볼륨 조절 가능하도록 설정
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveAllListeners(); // 기존 이벤트 제거
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f); // 저장된 볼륨 값 적용
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners(); // 기존 이벤트 제거
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f); // 저장된 볼륨 값 적용
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
            bgmSlider.onValueChanged.AddListener(SetBGMVolume); // 슬라이더 이벤트 추가
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume); // 슬라이더 이벤트 추가
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

            // 효과음 미리 듣기: 이미 재생 중이면 중단하고, 미리 듣기 코루틴 실행
            sfxAudioSource.Stop();
            StartCoroutine(PreviewSFX());
        }
    }

    private IEnumerator PreviewSFX()
    {
        // 미리 듣기를 위한 클립이 sfxAudioSource에 이미 할당되어 있어야 합니다.
        if (sfxAudioSource.clip != null)
        {
            sfxAudioSource.time = 0f;
            PlaySound(SoundType.Option);
            // 소리설정 패널이 켜져있다면 istime = 2 f 패널이 꺼져있다면 time =0 
            yield return new WaitForSeconds(2f); // 2초 동안 재생
            sfxAudioSource.Stop();
        }
    }

    private void SetAudioSource()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.loop = true;
            if (!bgmAudioSource.isPlaying) // 중복 실행 방지
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
