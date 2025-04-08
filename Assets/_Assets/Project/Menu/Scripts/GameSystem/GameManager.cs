using UnityEngine;
using static AudioManager;

public class GameManager : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; //�̱����� �Ҵ�� static ����

    public float score = 0;
    public bool isEating;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UIManager.Instance.ScoreUI(0);
    }
    //������ ������Ŵ
    public void SetScore(float newScore)
    {
        score += newScore;
        UIManager.Instance.ScoreUI(newScore);
        AudioManager.Instance.PlaySound(SoundType.Eat);
    }

    //���� ������ ��ȯ��
    public float GetScore()
    {
        return score; // ������ ��ȯ
    }

}
