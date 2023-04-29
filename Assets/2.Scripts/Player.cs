using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    /* ============== INSPECTOR MOVEMENT ============== */
    [Header("Movement")]
    [Tooltip("Move Speed of the character")]
    [SerializeField] private float walkSpeed = 8f;

    [Tooltip("Run Speed of the character")]
    [SerializeField] private float runSpeed = 12f;

    [Tooltip("How much character can jump")]
    [SerializeField] private float jumpForce = 2.5f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(3f, 10f)]
    [SerializeField] private float rotationSpeed = 10f;

    [Tooltip("Gravity Acceleration")]
    [SerializeField] private float gravity = -9.8f;

    /* ============== INSPECTOR STATS ============== */
    [Header("Statistics")]
    [SerializeField] private int level = 1;
    [SerializeField] private int experienceNeeded = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxMana = 100;

    /* ============== INSPECTOR REFERENCES ============== */
    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private ProgressBar healthBar;
    [SerializeField] private ProgressBar manaBar;
    [SerializeField] private ProgressBar expBar;
    [SerializeField] private PopupText popupTextPrefab;

    /* ============== PLAYER COMPONENTS ============== */
    private CharacterController characterController;
    private Rigidbody playerRigidBody;
    private PlayerInput playerInput;

    /* ============== VARIABLES ============== */
    private Vector3 moveDir;
    private Vector3 camRight;
    private Vector3 camForward;
    private float fallVelocity;
    private int currentHealth;
    private int currentMana;
    private int currentExperience = 0;

    /* ============== ANIMATION BOOLEANS ============== */
    private bool isWalking;
    private bool isJumping;
    private bool isGrounded;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start() {
        // Initialize Health
        currentHealth = maxHealth;
        if(healthBar) healthBar.SetMaxValue(maxHealth);
        // Initialize Mana
        currentMana = maxMana;
        if(manaBar) manaBar.SetMaxValue(maxMana);
    }

    private void Update() {
        MoveWithCharacterController();
        Jump(); 
    }

    /* ============== MOVEMENT CODE ============== */
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
        camRight = cameraTransform.right;
        camForward = cameraTransform.forward;

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

    /* ============== STATS CODE ============== */
    public void TakeDamage(int _damage, bool _isCritical){
        // Reduce the current health by the amount of damage sustained.
        currentHealth -= _damage;

        // Sets the new current health value to the health bar.
        healthBar.SetCurrentValue(currentHealth);

        // Triggers a popup text with the amount of damage sustained.
        if(popupTextPrefab != null){
            showPopupTextPrefab(_damage, _isCritical);
        }
    }

    public void ConsumeMana(int _mana){
        currentMana -= _mana;
        manaBar.SetCurrentValue(currentMana);
    }

    public void GainExperience(int _expPoints){
        currentExperience += _expPoints;
        expBar.SetCurrentValue(currentExperience);
    }

    private void showPopupTextPrefab(int _damage, bool _isCritical){
        // Instantiate the Popup Text Prefab and set this transform as parent.
        PopupText popupText = Instantiate(popupTextPrefab, transform.position, Quaternion.identity, transform);
        popupText.Setup(_damage, _isCritical);
    }
}