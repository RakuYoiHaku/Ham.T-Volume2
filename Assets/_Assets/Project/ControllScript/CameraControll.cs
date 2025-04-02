using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private CinemachineFreeLook freeLook;
    public float targetYAxisValue = 0.5f;  // ��ǥ YAxis �� (�ʱⰪ�� 0.3���� ����)
    private float dampingSpeed = 1.0f;  // damping �ӵ� (�ε巯�� ��ȯ)
    private float resetDelay = 1.0f;  // ��ũ�� �� �ǵ��ƿ��� �ð� ���� (��)
    private float timeSinceScroll = 0f;  // ��ũ�� �� ��� �ð� ����

    void Awake()
    {
        // CinemachineFreeLook�� ã�Ƽ� freeLook�� �Ҵ�
        freeLook = GetComponent<CinemachineFreeLook>();

        if (freeLook == null)
        {
            Debug.LogError("CinemachineFreeLook ������Ʈ�� ã�� �� �����ϴ�!");
        }

        freeLook.m_YAxis.Value = targetYAxisValue;  // �ʱ� Y �� ����
    }

    void Start()
    {
        // ���콺�� Y�� �� ������ ��Ȱ��ȭ
        freeLook.m_YAxis.m_InputAxisName = "";  // Y Axis�� �Է��� ��Ȱ��ȭ
    }

    void Update()
    {
        ScrollControl();  // ��ũ�� ������ ó��
    }

    public void ScrollControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");  // ��ũ�� �� ��������

        if (scroll != 0)
        {
            // ��ũ�� �Է� �� YAxis �� ���� (��ũ�ѷ� ��ǥ �� ����)
            targetYAxisValue += scroll * 2.0f;  // �ΰ��� ����

            // Y���� Ŭ�����Ͽ� ���� ���� (0 ~ 1)
            targetYAxisValue = Mathf.Clamp(targetYAxisValue, 0f, 1f);

            // ��ũ�� �Է� �� �ð��� 0���� ����
            timeSinceScroll = 0f;
        }
        else
        {
            // ��ũ�� �Է��� ������, ��� �ð��� ���� �ð� �̻��� ���� õõ�� �ǵ�����
            if (timeSinceScroll < resetDelay)
            {
                timeSinceScroll += Time.deltaTime;  // ��� �ð� ����
            }
            else
            {
                // ���� �ð� ��� ��, Y�� ���� ������� õõ�� �ǵ�����
                targetYAxisValue = Mathf.Lerp(targetYAxisValue, 0.3f, Time.deltaTime * dampingSpeed);
            }
        }

        // YAxis ���� �ε巴�� ��ȯ (damping ����)
        freeLook.m_YAxis.Value = Mathf.Lerp(freeLook.m_YAxis.Value, targetYAxisValue, Time.deltaTime * dampingSpeed);
    }
}
