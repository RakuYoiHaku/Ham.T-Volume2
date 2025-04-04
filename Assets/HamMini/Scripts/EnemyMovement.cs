using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // ���� �ӵ�
    public float moveRange = 4f; // �̵� ����

    private Vector3 startPosition; // ���� ��ġ
    private bool movingRight = true; // ���������� �̵� ������ ����

    public bool startL = true;
    private Animator animator;  // Animator ���� �߰�

    void Start()
    {
        startPosition = transform.position; // ���� ��ġ�� ���

        // Animator ��������
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
        // �¿�� �̵� (���� ����)
        float moveDirection = movingRight ? 1 : -1; // �������̸� 1, �����̸� -1

        // �̵�
        if (moveDirection > 0)
            animator.SetBool("isWalkingL", true);
        else
            animator.SetBool("isWalkingL", false);

        transform.Translate(Vector3.right * speed * moveDirection * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            // ���� ����
            movingRight = !movingRight;

            startPosition = transform.position;
        }

    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }


}