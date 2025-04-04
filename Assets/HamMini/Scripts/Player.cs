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

    private MiniGameAudioManager audioManager;  // AudioManager를 위한 변수

    public GameObject gameOverCanvas;  // 게임 오버 시 활성화할 캔버스
    public GameObject gameClearCanvas; // 게임 클리어 시 활성화할 캔버스

    private bool isGamePaused = false;  // 게임이 일시 정지되었는지 체크

    private Animator animator;  // Animator 변수 추가

    public GameObject doorC;
    public GameObject doorO;

    public GameObject leverOn;

    public GameObject[] cats;  // 적 오브젝트들을 담을 배열

    void Start()
    {
        UpdateScoreText();
        // AudioManager 찾기
        audioManager = FindObjectOfType<MiniGameAudioManager>();

        // Animator 가져오기
        animator = GetComponent<Animator>();

        cats = GameObject.FindGameObjectsWithTag("Cat");
    }

    
    void Update()
    {
        if (!isGamePaused)
        {
            Move();  // 게임이 멈추지 않으면 이동 처리
        }
    }

    private void Move()
    {
        speed_vec.x = Input.GetAxis("Horizontal") * speed;
        speed_vec.y = Input.GetAxis("Vertical") * speed;

        GetComponent<Rigidbody2D>().velocity = speed_vec;

        // 입력 값으로 애니메이션 제어
        if (speed_vec.x != 0 || speed_vec.y != 0)  // 사용자가 이동 입력을 할 때 애니메이션 실행
        {
            animator.SetBool("isWalking", true);
        }
        else  // 이동하지 않으면 애니메이션 멈춤
        {
            animator.SetBool("isWalking", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Seed"))
        {
            Destroy(collision.gameObject);
            IncreaseScore();  // 점수 증가 함수 호출

            audioManager.PlaySeedSound();  // Seed와 닿았을 때 효과음 재생

        }
        else if (collision.CompareTag("Cat"))
        {
            audioManager.PlayCatSound();   // Cat과 닿았을 때 효과음 재생
            GameOver();  // 게임 오버 처리

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
                    enemyMovement.IncreaseSpeed(1f);  // 속도 2 증가
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
        audioManager.StopBackgroundMusic();  // 배경음악 멈추기
        Time.timeScale = 0f;  // 게임 일시 정지 (시간 멈춤)
        isGamePaused = true;  // 게임 일시 정지 상태로 변경

        // 게임 오버 캔버스 활성화
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    private void GameClear()
    {
        audioManager.PlayGameClearSound();  // 게임 클리어 효과음 재생
        Time.timeScale = 0f;  // 게임 일시 정지 (시간 멈춤)
        isGamePaused = true;  // 게임 일시 정지 상태로 변경

        // 게임 클리어 캔버스 활성화
        if (gameClearCanvas != null)
        {
            gameClearCanvas.SetActive(true);
        }
    }

    #region 점수 제어
    private void IncreaseScore()
    {
        score++;  // 점수 증가
        UpdateScoreText();  // 점수 텍스트 업데이트
    }

    private void UpdateScoreText()
    {
        game_Score.text = "Seed : " + score.ToString() + " / " + max_score;  // 텍스트 UI에 점수 표시
        Clear_Score.text = "Seed : " + score.ToString() + " / " + max_score;  // 클리어텍스트 UI에 점수 표시
    }
    #endregion

}
