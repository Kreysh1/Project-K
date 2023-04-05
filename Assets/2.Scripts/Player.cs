using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    /* ============== SHOWN ON INSPECTOR ============== */
    [Header("Player")]
    [Tooltip("Move Speed of the character")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    // [Tooltip("Sprint Speed of the character")]
    // [SerializeField] private float sprintSpeed = 10f;

    [Tooltip("How much character can jump")]
    [SerializeField] private float jumpForce;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(3f, 10f)]
    [SerializeField] private float rotationSpeed = 10f;

    [Tooltip("Gravity Acceleration")]
    [SerializeField] private float gravity = -9.8f;

    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform cameraObject;

    /* ============== COMPONENTS REFERENCES ============== */
    private CharacterController characterController;
    private Rigidbody playerRigidBody;
    private PlayerInput playerInput;

    /* ============== VARIABLES ============== */
    private bool isWalking;
    private bool isJumping;
    private bool isGrounded;
    private Vector3 moveDir;
    private Vector3 camRight;
    private Vector3 camForward;
    private float fallVelocity;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        MoveWithCharacterController();
        Jump(); 
    }   

    private void MoveWithCharacterController(){
        // Gets a normalized Vector2 from GameInput Class.
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        moveDir = inputVector.x * camRight + inputVector.y * camForward;

        // Check if character is walking
        isWalking = (moveDir != Vector3.zero);     

        CamDirection();

        // Rotate the player smoothly
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        SetGravity();

        if(gameInput.OnRun()){
            // Running
            characterController.Move(moveDir * runSpeed * Time.deltaTime);
        }
        else{
            // Walking
            characterController.Move(moveDir * walkSpeed * Time.deltaTime);
        }
        
    }

    private void SetGravity(){
        if (characterController.isGrounded){
            // fallVelocity -= gravity * Time.deltaTime;
            fallVelocity -= 0;
            moveDir.y = fallVelocity;      
        }
        else{
            fallVelocity += gravity * Time.deltaTime;
            moveDir.y = fallVelocity;  
        }
    }

    private void CamDirection(){
        camRight = cameraObject.right;
        camForward = cameraObject.forward;

        camRight.y = 0;
        camForward.y = 0;

        camRight = camRight.normalized;
        camForward = camForward.normalized;
    }

    private void Jump(){
        // Check if character is grounded
        isGrounded = characterController.isGrounded;
        isJumping = !characterController.isGrounded;

        if (characterController.isGrounded && gameInput.OnJump()){
            fallVelocity = jumpForce;
            moveDir.y = fallVelocity;
        }
    }

    // BOOLEANS OF ANIMATIONS
    public bool IsWalking(){
        return isWalking;
    }

    public bool IsJumping(){
        return isJumping;
    }

    public bool IsGrounded(){
        return isGrounded;
    }

}