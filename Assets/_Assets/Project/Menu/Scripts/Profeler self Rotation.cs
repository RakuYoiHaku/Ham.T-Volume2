using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfelerselfRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 450f;

    Vector3 _origin;

    private void Awake()
    {
        _origin = transform.eulerAngles;
    }

    private void Update()
    {
        //���� ȸ������ ������
        Vector3 rotation = transform.eulerAngles;

        //���� ȸ������ ȸ�� �ӵ��� ���� ������ ����
        rotation.y += rotationSpeed * Time.deltaTime;

        //ȸ������ transform�� ����
        transform.localRotation = Quaternion.Euler(_origin.x, rotation.y, _origin.z);
    }
}
