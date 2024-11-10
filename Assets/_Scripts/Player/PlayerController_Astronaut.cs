using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Events;
using System;
using Unity.Mathematics;

public class PlayerController_Astronaut : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public Animator animator { get; private set; }
    public SkinnedMeshRenderer[] meshRenderers { get; private set; }

    public bool jumpPressed;
    public bool Grounded;

    
    private PlayerControls _playerControls;
    private InputAction _move;
    
    private Vector2 _moveInput;
    private Vector3 _forceDirection = Vector3.zero;

    [SerializeField]
    private float _movementForce = 1f;
    [SerializeField]
    private float _jumpForce = 5f;

    [SerializeField]
    private float _runMaxSpeed = 5f;

    [SerializeField]
    private Camera playerCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        _playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        _move = _playerControls.Astronaut.Move;

        _playerControls.Astronaut.Jump.started += Jump_started;
        _playerControls.Astronaut.Jump.performed += Jump_performed;
        _playerControls.Astronaut.Jump.canceled += Jump_canceled;
        
        _playerControls.Astronaut.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Astronaut.Jump.started -= Jump_started;
        _playerControls.Astronaut.Jump.performed -= Jump_performed;
        _playerControls.Astronaut.Jump.canceled -= Jump_canceled;
        
        _playerControls.Astronaut.Disable();
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

        if (_moveInput.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f )
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

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

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

    #region INPUT CALLBACKS
    private void Run_started(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
    }

    private void Run_performed(InputAction.CallbackContext context)
    {
        
    }

    private void Run_canceled(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
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
