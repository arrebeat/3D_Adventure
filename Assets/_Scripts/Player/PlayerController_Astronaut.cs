using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Events;
using System;
using Unity.Mathematics;
using System.Linq;

public class PlayerController_Astronaut : MonoBehaviour//, IDamageable
{
    public Rigidbody rb { get; private set; }
    public Animator animator { get; private set; }
    public SkinnedMeshRenderer[] meshRenderers { get; private set; }
    public CheckpointManager checkpointManager { get; private set; }
    public EffectsManager effectsManager { get; private set; }
    private Collider _collider;
    private ActionHealthPack _healthPack;

    public bool jumpPressed;
    public bool Grounded;
    public bool isMoving;

    
    private PlayerControls _playerControls;
    private InputAction _move;
    
    private Vector2 _moveInput;
    private Vector3 _forceDirection = Vector3.zero;

    [SerializeField]
    private float _movementForce = 1f;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _fallForce = 5f;

    [SerializeField]
    private float _runMaxSpeed = 5f;

    [SerializeField]
    private Camera playerCamera;

    [Header("Damage")]
    public HealthBase healthBase { get; private set; }
    public ScreenShake screenShake { get; private set; }
    public bool isDead = false;
    public Color damageFlashColor;
    public float damageFlashDuration = .1f;
    [SerializeField]
    private List<FlashColor> _flashColors;
    public float timeToRespawn = 1f;

    void OnValidate()
    {
        _collider = GetComponent<Collider>();
        _flashColors = GetComponentsInChildren<FlashColor>().ToList();
        healthBase = GetComponent<HealthBase>();
        _healthPack = GetComponent<ActionHealthPack>();
        screenShake = GetComponent<ScreenShake>();
        checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        effectsManager = GameObject.Find("EffectsManager").GetComponent<EffectsManager>();
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        //GameObject checkpointManagerObject = GameObject.Find("CheckpointManager");

        _playerControls = new PlayerControls();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += Kill;
    }

    private void OnEnable()
    {
        _move = _playerControls.Astronaut.Move;

        _playerControls.Astronaut.Move.started += Move_started;
        _playerControls.Astronaut.Move.performed += Move_performed;
        _playerControls.Astronaut.Move.canceled += Move_canceled;

        _playerControls.Astronaut.Jump.started += Jump_started;
        _playerControls.Astronaut.Jump.performed += Jump_performed;
        _playerControls.Astronaut.Jump.canceled += Jump_canceled;

        _playerControls.Astronaut.Recover.started += Recover;
        
        _playerControls.Astronaut.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Astronaut.Move.started -= Move_started;
        _playerControls.Astronaut.Move.performed -= Move_performed;
        _playerControls.Astronaut.Move.canceled -= Move_canceled;

        _playerControls.Astronaut.Jump.started -= Jump_started;
        _playerControls.Astronaut.Jump.performed -= Jump_performed;
        _playerControls.Astronaut.Jump.canceled -= Jump_canceled;
        
        _playerControls.Astronaut.Disable();
    }

    void Start()
    {
        animator.SetTrigger("idle");
    }

    void Update()
    {
        Grounded = isGrounded();
    }

    void FixedUpdate()
    {
        LookAt();
        Move();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (_moveInput.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private void Move()
    {
        _moveInput = _playerControls.Astronaut.Move.ReadValue<Vector2>();

        _forceDirection += _moveInput.x * GetCameraRight(playerCamera) * _movementForce;
        _forceDirection += _moveInput.y * GetCameraForward(playerCamera) * _movementForce;
        
        rb.AddForce(_forceDirection, ForceMode.Impulse);   
        _forceDirection = Vector3.zero;

        if (!isDead && rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * _fallForce * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        
        if (horizontalVelocity.sqrMagnitude > _runMaxSpeed * _runMaxSpeed)
            rb.velocity = horizontalVelocity.normalized * _runMaxSpeed + Vector3.up * rb.velocity.y;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private bool isGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }
    
    public void Damage(HealthBase h)
    {
        _flashColors.ForEach(i => i.flashColor = damageFlashColor);
        _flashColors.ForEach(i => i.flashDuration = damageFlashDuration);
        _flashColors.ForEach(i => i.Flash());
        
        effectsManager.VignetteEffect();

        screenShake.CameraShake();
    }
    public void Damage(int dmg, Vector3 dir)
    {
        //Damage(dmg);
    }

    public void Kill(HealthBase h)
    {
        _playerControls.Astronaut.Disable();
        
        rb.useGravity = false;
        _collider.enabled = false;

        if (!isDead) 
        {
            isDead = true;
            animator.SetTrigger("death");
        }

        Invoke(nameof(Respawn), timeToRespawn);
    }

    private void Recover(InputAction.CallbackContext context)
    {
        _healthPack.ConsumeHealthPack();
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        _playerControls.Astronaut.Enable();
        healthBase.ResetHp();

        isDead = false;
        rb.useGravity = true;
        _collider.enabled = true;

        animator.SetTrigger("idle");

        if (checkpointManager.HasCheckpoint())
        {
            transform.position = checkpointManager.GetLastCheckpointPosition();
        }
    }

    #region INPUT CALLBACKS
    private void Move_started(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        isMoving = true;
        animator.SetTrigger("run");
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        
    }

    private void Move_canceled(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        isMoving = false;
        animator.SetTrigger("idle");
    }

    private void Jump_started(InputAction.CallbackContext context)
    {
        jumpPressed = true;

        if (isGrounded())
        {
            _forceDirection += Vector3.up * _jumpForce;
        }
    }


    private void Jump_performed(InputAction.CallbackContext context)
    {
        
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        jumpPressed = false;
    }

    #endregion

}
