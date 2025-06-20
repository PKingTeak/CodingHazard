﻿using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region InputSystem
    public PlayerInputs playerInput { get; private set; }
    public PlayerInputs.PlayerActions playerActions { get; private set; }
    public bool IsInputBlocked { get; private set; } = false;
    #endregion
    
    #region State Machine & State Flags
    public PlayerStateMachine stateMachine { get; private set; }
    
    [Header("State Flags")]
    public bool isWalking = false;
    public bool isWalkingHold => playerActions.Walk.IsPressed();
    public bool isMoving = false;
    public bool isCrouching = false;
    public bool isSprinting = false;
    public bool canSprint = false;
    public bool isSprintHold => (playerActions.Sprint.IsPressed() && (Condition.stamina.curValue > 0f));
    public bool isReloading = false;

    public bool isReloadPressed
    {
        get
        {
            if (playerActions.Reloading.WasPressedThisFrame())
            {
                if (WeaponManager.CurrentWpeaWeapon is Gun gun)
                {
                    if (gun.CurAmmo < gun.MaxAmmo && gun.SpareAmmo > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public bool isAiming = false;
    public bool isAimHold => playerActions.Aiming.IsPressed();
    public bool isAttacking = false;
    public bool isJumping = false;
    public bool isJumpPressed => playerActions.Jump.WasPressedThisFrame();
    public bool isInteractPressed => playerActions.Interact.WasPressedThisFrame();

    #endregion

    #region Camera Look
    // [SerializeField, Range(-90f, 0f)] private float minXLook = -60f;
    // [SerializeField, Range(0f, 90f)] private float maxXLook = 30f;
    // [SerializeField, Range(50f, 300f)] private float lookSensitivity = 100f;
    public Transform playerTrans;
    public Transform camTrans;
    // private float xRotation = 0f;
    #endregion

    #region Camera Move
    // private Vector2 curMovementInput;
    // [SerializeField] private float moveSpeed;
    [SerializeField] public FPSVirtualCamera fpsVirtualCamera;
    #endregion

    #region Components
    public PlayerCondition Condition { get; private set; }
    public PlayerInteraction Interaction { get; private set;  }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    #endregion

    public WeaponManager WeaponManager { get; private set; }

    private void Awake()
    {
        playerInput = new PlayerInputs();
        playerActions = playerInput.Player;
        playerActions.Enable();

        stateMachine = new PlayerStateMachine(this);
        Condition = GetComponent<PlayerCondition>();
        Controller = GetComponent<CharacterController>();
        Interaction = GetComponent<PlayerInteraction>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Animator = GetComponentInChildren<Animator>();
        WeaponManager = GetComponent<WeaponManager>();
        


        stateMachine.FPScamTransform = fpsVirtualCamera.transform;

        PlayerEvent.Swap += (index) =>
        {
            stateMachine.ChangeState(new PlayerIdleState(stateMachine));
            
            AudioManager.Instance.StopAudio(AudioID.Reload);
        };

        playerInput.Player.Rifle.performed += index => PlayerEvent.Swap?.Invoke(0);
        playerInput.Player.Pistol.performed += index => PlayerEvent.Swap?.Invoke(1);
        playerInput.Player.Knife.performed += index => PlayerEvent.Swap?.Invoke(2);
        //012눌린 인덱스 번호를 주는 느낌으로 
    }

    public void BlockInput() => IsInputBlocked = true;
    public void UnblockInput() => IsInputBlocked = false;
    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canSprint = true;
        
        camTrans = fpsVirtualCamera.transform;
        playerTrans = transform;
        
        stateMachine.Initialize(new PlayerIdleState(stateMachine));
    }

    private void Update()
    {
        if (IsInputBlocked) return;

        // Player 시선에 따른 카메라 이동
        //Look();
        FireInput();
        fpsVirtualCamera.MouseDelta = playerActions.Look.ReadValue<Vector2>();

        if (Condition.stamina.CurValue <= 0f)
            canSprint = false;

        if (Condition.stamina.CurValue > 30f)
            canSprint = true;

        // Crouch 판정 및 처리
        if (playerActions.Sit.WasPressedThisFrame())
        {
            isCrouching = !isCrouching; // 토글
            Crouch(isCrouching);
        }
        
        // Interact 처리
        if (isInteractPressed)
        {
            Interaction.OnInteractInput();
        }
        
        stateMachine.CurrentState.Update();
    }
    
    // Animation Clip 길이 구하기
    // public float GetAnimationClipLength(string clipName)
    // {
    //     var clips = Animator.runtimeAnimatorController.animationClips;
    //
    //     foreach (var clip in clips)
    //     {
    //         if (clip.name == clipName)
    //             return clip.length;
    //     }
    //
    //     Debug.LogWarning($"Clip '{clipName}' not found!");
    //     return 2f; // fallback
    // }
    
    /*private void Look()
    {
        Vector2 mouseDelta = playerActions.Look.ReadValue<Vector2>();
    
        float mouseX = mouseDelta.x * lookSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXLook, maxXLook); // 위 아래 제한

        camTrans.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 상하
        playerTrans.Rotate(Vector3.up * mouseX); // 좌우
    }*/

    void Crouch(bool isCrouch)
    {
        if (isCrouch)
        {
            Debug.Log("Crouch");
            // 추후 isCrouching 값에 따라 조절하도록 구조 조정할 것
            Animator.SetBool("isCrouching", true);
            Controller.height = 1.1f;
            Controller.center = new Vector3(0, 0.6f, 0f);
            fpsVirtualCamera.SetCrouchAdjustment(new Vector3(0, -0.8f, 0f));
        }
        else
        {
            Debug.Log("UnCrouch");
            Animator.SetBool("isCrouching", false);
            Controller.height = 1.9f;
            Controller.center = new Vector3(0, 1f, 0f);
            fpsVirtualCamera.SetCrouchAdjustment(Vector3.zero);
        }
        
        // Crouch일 때의 카메라 이동 추가
    }
    
    public void Attack()
    {
        PlayerEvent.OnAttack?.Invoke();
        //공격 입력시 호출해주고 무기에서 Fire구독해서 사용할예정
        
        /*  // 현재 무기가 근접 무기일 경우 MeleeAttackState
         * if (현재 무기 == 근접 무기)
         *      stateMachine.ChangeState(new PlayerMeleeAttackState(stateMachine));
        
            // 주/보조 무기(총)일 경우 총을 쏘는 Method 실행
        */
    }


    private void FireInput()
    {
        if (!(WeaponManager.CurrentWpeaWeapon is Gun))
        {
            return;
        }

        Gun gun = (Gun)WeaponManager.CurrentWpeaWeapon;

        if (gun.CurFireMode == Gun.FireMode.Auto)
        {
            if (playerActions.Attack.IsPressed() && gun.GetFirerate())
            {
                Attack();
            }
        }

        else
        {
            if (playerActions.Attack.WasPressedThisFrame())
            {
                Attack();
            }
        }
       

        //뭐가 필요하지??
        //눌렸나? 클릭인가? 총종류가 뭔가 이걸 총에서 해야하나?? x 총은 쏘기만 담당하기로 



    }
}
