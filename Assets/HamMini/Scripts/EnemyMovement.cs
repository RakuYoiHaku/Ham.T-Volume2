using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // 적의 속도
    public float moveRange = 4f; // 이동 범위

    private Vector3 startPosition; // 시작 위치
    private bool movingRight = true; // 오른쪽으로 이동 중인지 여부

    public bool startL = true;
    private Animator animator;  // Animator 변수 추가

    void Start()
    {
        startPosition = transform.position; // 시작 위치를 기록

        // Animator 가져오기
        animator = GetComponent<Animator>();

        if (startL == true)
        {
            movingRight = true;
            animator.SetBool("isWalkingL", true);
        }
        else
        {
            movingRight = false;
            animator.SetBool("isWalkingL", false);
        }
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // 좌우로 이동 (방향 결정)
        float moveDirection = movingRight ? 1 : -1; // 오른쪽이면 1, 왼쪽이면 -1

        // 이동
        if (moveDirection > 0)
            animator.SetBool("isWalkingL", true);
        else
            animator.SetBool("isWalkingL", false);

        transform.Translate(Vector3.right * speed * moveDirection * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            // 방향 반전
            movingRight = !movingRight;

            startPosition = transform.position;
        }

    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }


}