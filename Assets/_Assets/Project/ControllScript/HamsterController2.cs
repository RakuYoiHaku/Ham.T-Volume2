using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HamsterController2 : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed;
    public float rotateSpeed;
    public float sprintSpeed;
    public float jumpPower;

    [Tooltip("���� ī�޶�")]
    public Camera _camera; // ���� ī�޶�

    [Tooltip("�����ϰ� ���� ������ ���� ��� ������Ʈ")]
    public GameObject targetObject; // �����ϰ� ���� ������ ���� ��� ������Ʈ

    private float _frontInput; // frontinput ��
    private Rigidbody _rigidbody;

    // �ִϸ��̼� ������ ���� bool��
    public bool isSprint; // Sprint ����
    public bool isJumping; // ���� ����
    public bool isClimbing;  // ���� Ÿ�� �ִ��� ����
    public bool isWalking; // Walk ����
    public bool stopClimbing; // Climbing ���� ��
    public bool startClimbing; // Climbing ���� ��
    public bool climbing;
    public bool inAir;

    private bool canMove = true;  // �̵� ���� ����
    private bool isNearObject = false; // �ݶ��̴��� ��Ҵ��� ����

    public void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (canMove)  // canMove�� true�� ���� �̵� ó��
        {
            if (climbing)
            {
                HandleClimbing();
            }

            HandleMove();
            HandleSprint();
            HandleJump();

            if (isNearObject && Input.GetKeyDown(KeyCode.E) && !climbing)
            {
                // E Ű�� ������ �� Ŭ���̹� ����
                if (!startClimbing) // �̹� ���� ���� �ƴϸ� Ŭ���̹��� ����
                {

                    startClimbing = true;
                    StartCoroutine(WaitStart(0.5f));  // 1�� ��� �� Ŭ���̹� ����
                }
            }
        }
    }
    #region Move/Sprint/Jump
    public void HandleMove()
    {
        if (!climbing)
        {
            isClimbing = false;
            _frontInput = Input.GetAxis("Vertical");


            if (_frontInput == 0)
            {
                isWalking = false;
                return;
            }

            float realMoveSpeed = isSprint ? sprintSpeed : moveSpeed;
            //sprint = sprintSpeed/ !sprint = moveSpeed
            // ���� Ÿ�� ���� ���� ī�޶� ȸ�� �� ��

            Vector3 forward = _camera.transform.forward;
            forward.y = 0f;
            forward.Normalize();
            // ī�޶��� ���⿡ ���� �÷��̾ ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forward),
                Time.deltaTime * rotateSpeed / 10f);

            Vector3 position = transform.position + forward * _frontInput * Time.deltaTime * realMoveSpeed;

            _rigidbody.MovePosition(position);
            isWalking = true;
        }
    }

    public void HandleSprint()
    {
        isSprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    public void HandleJump()
    {
        if (isJumping)
            return;

        if (Input.GetKey(KeyCode.Space) == false)
            return;

        if (climbing)
        {
            // ������ �� ���� Ÿ�� �ִ� ���¸� ����
            climbing = false;
            _rigidbody.useGravity = true; // ������ �������� Rigidbody �ٽ� Ȱ��ȭ
        }

        _rigidbody.AddForce(transform.up * jumpPower * 100f);
        isJumping = true;
    }
    #endregion

    #region CollisionEnter/Exit
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            _rigidbody.useGravity = true;

            inAir = false;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            isNearObject = true;
            inAir = false;
        }
        else if (collision.collider.CompareTag("Stop"))
        {
            stopClimbing = true;
            ClimbingStop();
            _rigidbody.useGravity = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            inAir = true;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            isNearObject = false;
            isClimbing = false;
            startClimbing = false;
            inAir = true;
        }
        else if (collision.collider.CompareTag("Stop"))
        {
            startClimbing = false;
        }
    }

    #endregion

    #region Climbing Start/Stop

    // ���� Ż �� ���Ϸθ� �����̰� �ϴ� �Լ�

    public void HandleClimbing()
    {
        _rigidbody.useGravity = false; // ���� Ż �� �߷� ��Ȱ��ȭ

        _frontInput = Input.GetAxis("Vertical");  // ���� �Է�

        // �Է��� ���� ���� climbing ���¸� true�� ����
        if (Mathf.Abs(_frontInput) > 0.1f)  // �Է��� ������ isClimbing�� true�� ����
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }

        // ���Ϸθ� �����̵��� ����
        Vector3 moveDirection = Vector3.up * _frontInput * Time.deltaTime * moveSpeed;
        _rigidbody.MovePosition(transform.position + moveDirection);
    }
    public void ClimbingStop()
    {
        climbing = false;
        transform.position = targetObject.transform.position; // ������ ����
        StartCoroutine(WaitStop(1f)); // Stop �� ���
        isClimbing = false; // Ŭ���̹� ���� ����
        startClimbing = false; // startClimbing�� false�� �����Ͽ� �ߺ� ȣ�� ����
    }
    private IEnumerator WaitStop(float waitTime)
    {
        canMove = false;  // 2�� ���� �Է� ���� �ʵ��� ����
        yield return new WaitForSeconds(waitTime);  // ��ٸ���
        canMove = true;  // �̵� �����ϵ��� ����
        stopClimbing = false; // stopClimbing �ִϸ��̼��� Ʈ����
    }
    private IEnumerator WaitStart(float waitTime)
    {
        canMove = false;  // 2�� ���� �Է� ���� �ʵ��� ����
        yield return new WaitForSeconds(waitTime);  // ��ٸ���
        canMove = true;  // �̵� �����ϵ��� ����

        startClimbing = false;
        climbing = true;
    }
    #endregion
}
