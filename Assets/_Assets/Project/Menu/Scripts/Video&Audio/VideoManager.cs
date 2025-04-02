using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance { get; private set; }
    //���� �÷��̾�
    public VideoPlayer videoPlayer;
    //���� Ÿ�� (2)
    public RawImage[] videoTarget = new RawImage[2];
    //���� Ŭ�� (2)
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
            // ���� ���� Ŭ�� ����
            videoPlayer.clip = videoClips[0];
            // ���� Ÿ�� (RenderTexture) ����
            videoPlayer.targetTexture = videoTarget[0].texture as RenderTexture;

            videoPlayer.isLooping = true;
            videoPlayer.Play();

            //BGM ���
            AudioManager.Instance.bgmAudioSource.Play();
        }
    }

    public void PlayLoadVideo()
    {
        if (videoPlayer != null)
        {
            // ���� ���� Ŭ�� ����
            videoPlayer.clip = videoClips[1];
            // ���� Ÿ�� (RenderTexture) ����
            videoPlayer.targetTexture = videoTarget[1].texture as RenderTexture;

            videoPlayer.isLooping = true;
            videoPlayer.Play();

            //BGM/SFX ����
            AudioManager.Instance.bgmAudioSource.Stop();
            AudioManager.Instance.sfxAudioSource.Stop();
        }
    }
}
