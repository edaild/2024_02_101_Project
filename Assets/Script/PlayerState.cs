using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerState 모든 플레이어 상태의 기본이 되는 추상 클래스
public abstract class PlayerState 
{
    protected PlayerStateMachine stateMachine;        // 상태 머신에 대한 참조
    protected PlayerController playerController;        // 플레이어 컨트롤러에 대한 참조
    protected PlayerAnimationManager animationManager; // 애니메이션 매니저를 가져온다.

    // 생성자 상태 머신과 플레이어 컨트롤러 참조 초기화
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
        this.animationManager = stateMachine.GetComponent<PlayerAnimationManager>();
    }

    // 가상 메서드를 : 하위 클래스에서 필요에 따라 오버로이드
    public virtual void Enter() { }     // 상태 진입 시 호출

    public  virtual void Exit() { }     // 상태 종류 시 호출

    public virtual void Update() { } // 매 프레임 호출
    public virtual void FixedUpdate() { }   // 고정 시간 긴격으로 호출 (물리 연산용)

    // 상태 전환과 조건을 체크하는 메서드
    protected void CheckTransitions()
    {
        if (playerController.isGrounded())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                stateMachine.TransitionTostate(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0) // 이동키가 눌렸을 때
            {
                stateMachine.TransitionTostate(new MovingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTostate(new IdleState(stateMachine));
            }
        }
        else
        {
            // 공중에 있을때 상태 전환 로직
            if (playerController.GetVerticalVelocity() > 0)     // 받아온 Y 축 속도 값이 + 일때
            {
                stateMachine.TransitionTostate(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTostate(new FallingState(stateMachine)); // 받아온 Y축 속도값이 - dlfeO [낙하 상태]
            }
        }
    }
}

//IdleState : 플레이어가 정지해 있는 상태
public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // 매 프래임 마다 상태 전환 조건 체크
    }
}

// MovingState : 플레이어가 이동 상태일때
public class MovingState : PlayerState
{
    private bool IsRunning;
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        // 달리기 입력 확인
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        CheckTransitions();         // 매 프래임 마다 상태 전환 조건 체크
    }
    public override void FixedUpdate() 
    {
        playerController.HandleMovement();      // 물리 기반 이동 처리
    }
}

// JumpingState : 플레이어가 점프 상태일때
public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // 매 프래임 마다 상태 전환 조건 체크
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();      // 물리 기반 이동 처리
    }
}

// FallingState : 플레이어가 낙하 중일때
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // 매 프래임 마다 상태 전환 조건 체크
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();      // 물리 기반 이동 처리
    }
}


