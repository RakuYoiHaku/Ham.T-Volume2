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

    private int score = 0;

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
    public void SetScore(int newScore)
    {
        score += newScore;
        UIManager.Instance.ScoreUI(score);
        AudioManager.Instance.PlaySound(SoundType.Eat);
    }

    //���� ������ ��ȯ��
    public int GetScore()
    {
        return score; // ������ ��ȯ
    }
}
