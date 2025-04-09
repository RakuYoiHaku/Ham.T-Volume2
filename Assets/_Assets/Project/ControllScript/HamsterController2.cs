using System.Collections;
using UnityEngine;

public class HamsterController2 : MonoBehaviour
{
    public bool allowInput = true;

    [SerializeField] float raycastDist = 0.01f;

    [Header("Player")]
    public float moveSpeed;
    public float rotateSpeed ;
    public float sprintSpeed;
    public float jumpPower;

    [Tooltip("���� ī�޶�")]
    public Camera _camera; // ���� ī�޶�

    [Tooltip("�����ϰ� ���� ������ ���� ��� ������Ʈ")]
    public GameObject targetObject; // �����ϰ� ���� ������ ���� ��� ������Ʈ

    private float _frontInput; // frontinput ��
    private Rigidbody _rigidbody;

    [Header("Animation")]
    // �ִϸ��̼� ������ ���� bool��
    public bool isSprint; // Sprint ����
    public bool isJumping; // ���� ����
    public bool isClimbing;  // ���� Ÿ�� �ִ��� ����
    public bool isWalking; // Walk ����
    public bool stopClimbing; // Climbing ���� ��
    public bool startClimbing; // Climbing ���� ��
    public bool climbing;
    public bool inAir;
    public bool awake;
    //public bool isGrounded;

    [Header ("-------------------------------------------------------")]
    public bool canMove = false;  // �̵� ���� ����
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
            if (!allowInput) return;

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
                    StartCoroutine(WaitStart(0.1f));  // 1�� ��� �� Ŭ���̹� ����
                }
            }
        }
        else
        {
            // Ű���� �Է� ���� �� awake�� true�� �����ϰ�, 2�� ��� �� canMove Ȱ��ȭ
            if (!awake && (Input.anyKeyDown || Input.anyKey))  // Ű �Է��� ���� ��
            {
                awake = true;
                StartCoroutine(WaitForInputAndEnableMovement(2f));  // 2�� ��� �� �̵� �����ϰ� ����
            }
        }
    }

    void Update()
    {
        inAir = !IsGrounded();
    }

    bool IsGrounded()
    {
        // �Ʒ��� ����ĳ��Ʈ�� ���� �ٴڿ� ��Ҵ��� üũ
        return Physics.Raycast(transform.position, Vector3.down, raycastDist); // ���� �Ÿ��� �ٴ� üũ
    }

    // 2�� �� canMove�� Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator WaitForInputAndEnableMovement(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);  // 2�� ���
        canMove = true;  // �̵� ����
        awake = false;
    }



    #region Move/Sprint/Jump
   public void HandleMove()
{
    if (!climbing)
    {
        isClimbing = false;
        _frontInput = Input.GetAxis("Vertical");  // ���� ���� �Է� (W, S �Ǵ� ��, �Ʒ� ���� Ű)
        float horizontalInput = Input.GetAxis("Horizontal");  // �¿� ���� �Է� (A, D �Ǵ� ����, ������ ���� Ű)

        if (_frontInput == 0 && horizontalInput == 0) // ���Ͽ� �¿� �Է��� ��� ������ �ȴ� ���¸� ����
        {
            isWalking = false;
            return;
        }

        float realMoveSpeed = isSprint ? sprintSpeed : moveSpeed;

        // ī�޶��� ���⿡ ���� �÷��̾ ȸ�� (�� �������� �̵�)
        Vector3 forward = _camera.transform.forward;
        forward.y = 0f;  // y ������ ����
        forward.Normalize();  // ���͸� ���� ���ͷ� ����ȭ

        // ī�޶��� ������ ���� ���� ��� (�÷��̾ ī�޶� �������� �¿�� �̵��ϵ���)
        Vector3 right = _camera.transform.right;
        right.y = 0f;  // y ������ ����
        right.Normalize();  // ���͸� ���� ���ͷ� ����ȭ

        // �÷��̾��� �̵� ���� ��� (���� + �¿�)
        Vector3 moveDirection = forward * _frontInput + right * horizontalInput;
        moveDirection.Normalize();  // ���� ���͸� ����ȭ�Ͽ� �ӵ� �ϰ��� ����

        // ȸ�� ó�� (�÷��̾ �̵� �������� �ڿ������� ȸ��)
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection),
                Time.deltaTime * rotateSpeed / 10f);
        }

        // ���� �̵� ó��
        Vector3 position = transform.position + moveDirection * Time.deltaTime * realMoveSpeed;
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
        //inAir = true;
    }
    #endregion

    #region CollisionEnter/Exit
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            _rigidbody.useGravity = true;

            //inAir = false;
            //isGrounded =true;
        }
        else if (collision.collider.CompareTag("Curtain"))
        {
            isNearObject = true;
            //inAir = false;
            //isGrounded = true;
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
            //inAir = true;
            //isGrounded = false;
        }
        else if (collision.collider.CompareTag("Curtain"))
        {
            isNearObject = false;
            isClimbing = false;
            startClimbing = false;
            //inAir = true;
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
