using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_AstronautEbac : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    [Header("Debug float")]
    [SerializeField]
    private float _verticalSpeed = 0;
    [SerializeField]
    private bool _jumpPressed;
    [SerializeField]
    private bool _isMoving;
    [SerializeField]
    private Vector2 _moveInput;

    private PlayerControls _playerControls;
    private InputAction _move;


    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _move = _playerControls.Astronaut.Move;

        _move.started += Move_started;
        _move.performed += Move_performed;
        _move.canceled += Move_canceled;

        _playerControls.Astronaut.Enable();
    }

    private void OnDisable()
    {
        _move.started -= Move_started;
        _move.performed -= Move_performed;
        _move.canceled -= Move_canceled;

        _playerControls.Astronaut.Disable();
    }

    void Start()
    {
        animator.SetTrigger("idle");
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        _moveInput = _playerControls.Astronaut.Move.ReadValue<Vector2>();

        transform.Rotate(0, _moveInput.x * turnSpeed * Time.fixedDeltaTime, 0);

        var speedVector = transform.forward * _moveInput.y * speed;

        _verticalSpeed -= gravity * Time.fixedDeltaTime;        
        speedVector.y = _verticalSpeed;        
        characterController.Move(speedVector * Time.fixedDeltaTime);
    }
    
    #region INPUT CALLBACKS
    private void Move_started(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        _isMoving = true;
        animator.SetTrigger("run");
    }
    private void Move_performed(InputAction.CallbackContext context)
    {
        
    }
    private void Move_canceled(InputAction.CallbackContext context)
    {
        _isMoving = false;
        animator.SetTrigger("idle");
    }
    #endregion
}
