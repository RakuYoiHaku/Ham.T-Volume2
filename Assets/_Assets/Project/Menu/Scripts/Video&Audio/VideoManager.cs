using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance { get; private set; }
    //비디오 플레이어
    public VideoPlayer videoPlayer;
    //비디오 타겟 (2)
    public RawImage[] videoTarget = new RawImage[2];
    //비디오 클립 (2)
    public VideoClip[] videoClips = new VideoClip[2];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    private void Start()
    {
        PlayMainVideo();
    }

    private void PlayMainVideo()
    {
        if (videoPlayer != null && videoClips.Length > 0)
        {
            // 메인 비디오 클립 설정
            videoPlayer.clip = videoClips[0];
            // 비디오 타겟 (RenderTexture) 설정
            videoPlayer.targetTexture = videoTarget[0].texture as RenderTexture;

            videoPlayer.isLooping = true;
            videoPlayer.Play();

            //BGM 재생
            AudioManager.Instance.bgmAudioSource.Play();
        }
    }

    public void PlayLoadVideo()
    {
        if (videoPlayer != null)
        {
            // 메인 비디오 클립 설정
            videoPlayer.clip = videoClips[1];
            // 비디오 타겟 (RenderTexture) 설정
            videoPlayer.targetTexture = videoTarget[1].texture as RenderTexture;

            videoPlayer.isLooping = true;
            videoPlayer.Play();

            //BGM/SFX 멈춤
            AudioManager.Instance.bgmAudioSource.Stop();
            AudioManager.Instance.sfxAudioSource.Stop();
        }
    }
}
