
using UnityEngine;

// <CAY> //<YSA> �ּ� �� ���� �̸� ����

public class _PlayerInput : MonoBehaviour
{
    public string frontBackAxisName = "Vertical"; // �յ� �������� ���� �Է��� �̸�
    public string leftRightAxisName = "Horizontal"; // �¿� ȸ���� ���� �Է��� �̸�

    // Ű���� ���� ����� �����ϴ� ����
    // ������ �б�� �ٱ������� �����Ӱ� �� �� ������,
    // ������ �� ������ �� Ŭ���� �������� �����ϴ�.
    public float frontBack { get; private set; } // ������ �յ� ������ �Է°�
    public float leftRight { get; private set; } // ������ �¿� ������ �Է°�

    // �������� ����� �Է��� ����
    private void Update()
    {
        //frontBack, leftRight �Է� ���θ� �ǽð����� üũ

        //�յ� �Է°� �����ؼ� frontBack�� �ֱ�. (-1�̸� ����, 1�̸� ����)
        frontBack = Input.GetAxis("Vertical");

        //�¿� �Է°� �����ؼ� leftRight�� �ֱ�
        leftRight = Input.GetAxis("Horizontal");
    }

    //���콺 ��ũ�� �ϸ� ī�޶� �������
}
