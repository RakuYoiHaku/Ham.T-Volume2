using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerCameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float dragSpeed = 3.0f; // ȸ�� �ӵ�

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        // ���콺 �Է¿� ���� �ڵ� ȸ�� ��Ȱ��ȭ
        freeLookCamera.m_XAxis.m_InputAxisName = ""; // �� ���ڿ��� ����
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    void Update()
    {
        HandleCameraRotation();
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư ���� ��
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ���� ��ư �� ��
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // X�� ȸ�� (���� ����)
            freeLookCamera.m_XAxis.Value += delta.x * dragSpeed * Time.deltaTime;

            //// Y�� ȸ�� (���� ����)
            //freeLookCamera.m_YAxis.Value -= delta.y * dragSpeed * 0.01f;
        }
    }
}
