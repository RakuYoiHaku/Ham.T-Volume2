using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 speed_vec;

    private int score = 0;
    public int max_score = 5;
    [SerializeField] TextMeshProUGUI game_Score;
    [SerializeField] TextMeshProUGUI Clear_Score;

    private MiniGameAudioManager audioManager;  // AudioManager�� ���� ����

    public GameObject gameOverCanvas;  // ���� ���� �� Ȱ��ȭ�� ĵ����
    public GameObject gameClearCanvas; // ���� Ŭ���� �� Ȱ��ȭ�� ĵ����

    private bool isGamePaused = false;  // ������ �Ͻ� �����Ǿ����� üũ

    private Animator animator;  // Animator ���� �߰�

    public GameObject doorC;
    public GameObject doorO;

    public GameObject leverOn;

    public GameObject[] cats;  // �� ������Ʈ���� ���� �迭

    void Start()
    {
        UpdateScoreText();
        // AudioManager ã��
        audioManager = FindObjectOfType<MiniGameAudioManager>();

        // Animator ��������
        animator = GetComponent<Animator>();

        cats = GameObject.FindGameObjectsWithTag("Cat");
    }

    
    void Update()
    {
        if (!isGamePaused)
        {
            Move();  // ������ ������ ������ �̵� ó��
        }
    }

    private void Move()
    {
        speed_vec.x = Input.GetAxis("Horizontal") * speed;
        speed_vec.y = Input.GetAxis("Vertical") * speed;

        GetComponent<Rigidbody2D>().velocity = speed_vec;

        // �Է� ������ �ִϸ��̼� ����
        if (speed_vec.x != 0 || speed_vec.y != 0)  // ����ڰ� �̵� �Է��� �� �� �ִϸ��̼� ����
        {
            animator.SetBool("isWalking", true);
        }
        else  // �̵����� ������ �ִϸ��̼� ����
        {
            animator.SetBool("isWalking", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Seed"))
        {
            Destroy(collision.gameObject);
            IncreaseScore();  // ���� ���� �Լ� ȣ��

            audioManager.PlaySeedSound();  // Seed�� ����� �� ȿ���� ���

        }
        else if (collision.CompareTag("Cat"))
        {
            audioManager.PlayCatSound();   // Cat�� ����� �� ȿ���� ���
            GameOver();  // ���� ���� ó��

        }
        else if (collision.CompareTag("Clear"))
        {
            GameClear();
        }
        else if (collision.CompareTag("Lever"))
        {
            Destroy(collision.gameObject);
            doorC.SetActive(false);
            doorO.SetActive(true);
            leverOn.SetActive(true);
        }
        else if (collision.CompareTag("Bw"))
        {
            foreach (GameObject enemy in cats)
            {
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.IncreaseSpeed(1f);  // �ӵ� 2 ����
                }
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Gw"))
        {
            Destroy(collision.gameObject);
            speed += 2;
        }

    }

    private void GameOver()
    {
        audioManager.StopBackgroundMusic();  // ������� ���߱�
        Time.timeScale = 0f;  // ���� �Ͻ� ���� (�ð� ����)
        isGamePaused = true;  // ���� �Ͻ� ���� ���·� ����

        // ���� ���� ĵ���� Ȱ��ȭ
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    private void GameClear()
    {
        audioManager.PlayGameClearSound();  // ���� Ŭ���� ȿ���� ���
        Time.timeScale = 0f;  // ���� �Ͻ� ���� (�ð� ����)
        isGamePaused = true;  // ���� �Ͻ� ���� ���·� ����

        // ���� Ŭ���� ĵ���� Ȱ��ȭ
        if (gameClearCanvas != null)
        {
            gameClearCanvas.SetActive(true);
        }
    }

    #region ���� ����
    private void IncreaseScore()
    {
        score++;  // ���� ����
        UpdateScoreText();  // ���� �ؽ�Ʈ ������Ʈ
    }

    private void UpdateScoreText()
    {
        game_Score.text = "Seed : " + score.ToString() + " / " + max_score;  // �ؽ�Ʈ UI�� ���� ǥ��
        Clear_Score.text = "Seed : " + score.ToString() + " / " + max_score;  // Ŭ�����ؽ�Ʈ UI�� ���� ǥ��
    }
    #endregion

}
