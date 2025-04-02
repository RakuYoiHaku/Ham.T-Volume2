using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private CinemachineFreeLook freeLook;
    public float targetYAxisValue = 0.5f;  // 목표 YAxis 값 (초기값을 0.3으로 고정)
    private float dampingSpeed = 1.0f;  // damping 속도 (부드러운 전환)
    private float resetDelay = 1.0f;  // 스크롤 후 되돌아오는 시간 지연 (초)
    private float timeSinceScroll = 0f;  // 스크롤 후 경과 시간 추적

    void Awake()
    {
        // CinemachineFreeLook을 찾아서 freeLook에 할당
        freeLook = GetComponent<CinemachineFreeLook>();

        if (freeLook == null)
        {
            Debug.LogError("CinemachineFreeLook 컴포넌트를 찾을 수 없습니다!");
        }

        freeLook.m_YAxis.Value = targetYAxisValue;  // 초기 Y 값 설정
    }

    void Start()
    {
        // 마우스로 Y축 값 변경을 비활성화
        freeLook.m_YAxis.m_InputAxisName = "";  // Y Axis의 입력을 비활성화
    }

    void Update()
    {
        ScrollControl();  // 스크롤 조작을 처리
    }

    public void ScrollControl()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");  // 스크롤 값 가져오기

        if (scroll != 0)
        {
            // 스크롤 입력 시 YAxis 값 변경 (스크롤로 목표 값 변경)
            targetYAxisValue += scroll * 2.0f;  // 민감도 증가

            // Y값을 클램프하여 범위 제한 (0 ~ 1)
            targetYAxisValue = Mathf.Clamp(targetYAxisValue, 0f, 1f);

            // 스크롤 입력 후 시간을 0으로 리셋
            timeSinceScroll = 0f;
        }
        else
        {
            // 스크롤 입력이 없으면, 경과 시간이 일정 시간 이상일 때만 천천히 되돌리기
            if (timeSinceScroll < resetDelay)
            {
                timeSinceScroll += Time.deltaTime;  // 경과 시간 증가
            }
            else
            {
                // 일정 시간 경과 후, Y축 값을 원래대로 천천히 되돌리기
                targetYAxisValue = Mathf.Lerp(targetYAxisValue, 0.3f, Time.deltaTime * dampingSpeed);
            }
        }

        // YAxis 값을 부드럽게 전환 (damping 적용)
        freeLook.m_YAxis.Value = Mathf.Lerp(freeLook.m_YAxis.Value, targetYAxisValue, Time.deltaTime * dampingSpeed);
    }
}
