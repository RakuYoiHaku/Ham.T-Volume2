using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerCameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float dragSpeed = 3.0f; // 회전 속도

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        // 마우스 입력에 의한 자동 회전 비활성화
        freeLookCamera.m_XAxis.m_InputAxisName = ""; // 빈 문자열로 설정
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    void Update()
    {
        HandleCameraRotation();
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 누를 때
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼 뗄 때
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // X축 회전 (수평 방향)
            freeLookCamera.m_XAxis.Value += delta.x * dragSpeed * Time.deltaTime;

            //// Y축 회전 (수직 방향)
            //freeLookCamera.m_YAxis.Value -= delta.y * dragSpeed * 0.01f;
        }
    }
}
