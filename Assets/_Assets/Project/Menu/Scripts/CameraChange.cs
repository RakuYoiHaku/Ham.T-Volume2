using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] CameraType camType;
    [SerializeField] GameObject hamster;
    [SerializeField] GameObject c_hamster;

    [SerializeField] SkinnedMeshRenderer hamsterMesh;
    [SerializeField] CinemachineDollyCart cart;
    [SerializeField] Transform hamsterPosition;

    private void OnTriggerEnter(Collider other)
    {
        switch(camType)
        {
            case CameraType.HAMSTER:
                // �ܽ��� �޽������� Ȱ��ȭ
                // �ܽ��� ��ġ�� ������������ �ű��
                // ���� īƮ �ӵ� 0
                // īƮ�� �޽������� ��Ȱ��ȭ

                hamster.SetActive(true);
                hamster.transform.position = hamsterPosition.position;

                cart.m_Speed = 0;
                c_hamster.SetActive(false);

                // �÷��̾� �̵� �Է� �ʱ�ȭ
                ResetPlayerMovement();

                break;

            case CameraType.AIR_PLANE:
                // �ܽ��� �޽������� ��Ȱ��ȭ
                // īƮ�� �޽������� Ȱ��ȭ
                // ���� īƮ �ӵ� �ø���

                hamster.SetActive(false);
                c_hamster.SetActive(true);
                cart.m_Speed = 5f;
                break;
        }    

        CameraSet.Instance.ChangeCamera(camType);
    }

    private void ResetPlayerMovement()
    {
          
    }
}
