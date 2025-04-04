using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HamsterAnimator : MonoBehaviour
{
    private Animator playerAnimator; //�÷��̾� ĳ������ �ִϸ�����
    private HamsterController2 hamsterController; // �÷��̾� �̵��� �˷��ִ� ������Ʈ


    public void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        hamsterController = GetComponent<HamsterController2>();
    }

    public void Update()
    {
        if (hamsterController == null) return;

        // �ִϸ��̼� ���� ����
        HandleAnimations();
    }

    public void HandleAnimations()
    {
        #region  Jump
        // ���� ���� (������ ����� ��)
        if (hamsterController.isJumping)
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }
        #endregion

        #region Walk �ִϸ��̼��� InAir�� false, Walk�϶� ����
        if (hamsterController.isWalking)
        {
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }
        #endregion

        #region Sprint
        if (hamsterController.isSprint)
        {
            playerAnimator.SetBool("IsSprint", true);
        }
        else
        {
            playerAnimator.SetBool("IsSprint", false);
        }
        #endregion


        #region Climbing
        if (hamsterController.climbing)
        {
            playerAnimator.SetBool("Climbing", true);
        }
        else
        {
            playerAnimator.SetBool("Climbing", false);
        }

        if (hamsterController.isClimbing)
        {
            playerAnimator.SetBool("IsClimbing", true);
        }
        else
        {
            playerAnimator.SetBool("IsClimbing", false);
        }


        if (hamsterController.stopClimbing)
        {
            playerAnimator.SetBool("StopClimbing", true);
        }
        else
        {
            playerAnimator.SetBool("StopClimbing", false);
        }

        if (hamsterController.startClimbing)
        {
            playerAnimator.SetBool("StartClimbing", true);
        }
        else
        {
            playerAnimator.SetBool("StartClimbing", false);
        }
        #endregion

        #region InAir InAir�� ���������� ����
        if (hamsterController.inAir)
        {
            playerAnimator.SetBool("InAir", true);
        }
        else
        {
            playerAnimator.SetBool("InAir", false);
        }
        #endregion

        #region awake
        if (hamsterController.awake)
        {
            playerAnimator.SetBool("Awake", true);
        }
        else
        {
            playerAnimator.SetBool("Awake", false);
        }
        #endregion
        
        if (GameManager.Instance.isEating)
        {
            playerAnimator.SetBool("IsEating", true);
            GameManager.Instance.isEating = false;
        }
        else
        {

            playerAnimator.SetBool("IsEating", false);
        }

        playerAnimator.SetFloat("CollectSeed", GameManager.Instance.score);

    }
}