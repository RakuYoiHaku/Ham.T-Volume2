using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAnimator : MonoBehaviour
{
    private Animator playerAnimator; //플레이어 캐릭터의 애니메이터
    private HamsterController2 hamsterController; // 플레이어 이동을 알려주는 컴포넌트

    //public bool isSprint; // Sprint 여부
    //public bool isJumping; // 점프 여부
    //public bool isClimbing;  // 벽을 타고 있는지 여부
    //public bool isWalking; // Walk 여부
    //public bool StopClimbing; // Climbing 종료 중

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        hamsterController = GetComponent<HamsterController2>();
    }

    private void Update()
    {
        if (hamsterController == null) return;

        // 애니메이션 상태 변경
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        #region  Jump
        // 점프 상태 (점프가 실행될 때)
        if (hamsterController.isJumping)
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }
        #endregion

        #region Walk 애니메이션은 InAir가 false, Walk일때 실행
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

        //// StartClimbing, StopClimbing 애니메이션은 한 번만 호출되도록 설정
        //if (hamsterController.startClimbing && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("StartClimbing"))
        //{
        //    playerAnimator.SetTrigger("StartClimbing");
        //}

        //if (hamsterController.stopClimbing)
        //{
        //    playerAnimator.SetTrigger("StopClimbing");
        //}
        #region InAir InAir만 켜져있을때 실행
        if (hamsterController.inAir)
        {
            playerAnimator.SetBool("InAir", true);
        }
        else
        {
            playerAnimator.SetBool("InAir", false);
        }
        #endregion
    }
}