using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{
    #region -------------- Member Variables ---------------------
    private PlayerInputData playerInputData;
    public PlayerInputData getPlayerInputData => playerInputData;
    private Animator playerAnimController;
    private Rigidbody rb;

    [Header("Player Variables")]
    [SerializeField] private float m_fVerticalJumpForce;
    [SerializeField] private float m_fMoveSpeed;
    [SerializeField] private float m_fDashForce;
    [SerializeField] private float m_fGroundedRadius;

    [Header("Movement Input")]
    [SerializeField] private Vector3 m_moveInput = Vector3.zero;
    [SerializeField] private Vector3 m_velocityVector = Vector3.zero;

    [SerializeField] private Transform checkGroundRay;
    [SerializeField] private LayerMask GroundedLayers;
    [SerializeField] private Camera playerCamera;

    [Header("Animation States")]
    [SerializeField]
    private bool isActive = false;
    public bool pause = false;
    public bool firing = false;
    public bool didDash = false;
    [SerializeField] private bool isGrounded;

    // Audio Source
    [Header("SFX")]
    private AudioSource audioSource;
    public AudioSource GetAudioSource => audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] public List<AudioClip> GetAudioClips => audioClips;

    // Animator Hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementZHash = Animator.StringToHash("MovementZ");
    public readonly int isGroundedHash = Animator.StringToHash("isGrounded");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int didFireHash = Animator.StringToHash("didFire");
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        playerInputData = new PlayerInputData();
        rb = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>();
        playerAnimController = GetComponent<Animator>();
        isGrounded = false;
        didDash = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isActive = true;
        InitPlayerActions();
        rb.velocity = Vector3.zero;
        //PausePanel.SetActive(false);

        GameManager.Instance.cursorActive = false;

        if (!GameManager.Instance.cursorActive)
        {
            pause = false;
            // PausePanel.SetActive(pause);
            AppEvents.InvokeOnPauseEvent(false);
        }
    }

    private void InitPlayerActions()
    {
        playerInputData.Player.Enable();
        playerInputData.Player.Move.performed += OnMove;
        playerInputData.Player.Move.canceled += OnMove;
        playerInputData.Player.Fire.started += OnFire;
        playerInputData.Player.Fire.canceled += OnFire;
        playerInputData.Player.Jump.started += OnJump;
        playerInputData.Player.Pause.started += OnPause;
        playerInputData.Player.Dash.started += OnDash;
        playerInputData.Player.Dash.canceled += OnDash;

    }

    private void OnEnable()
    {
        if (isActive)
        {
            InitPlayerActions();
        }
    }

    private void OnDisable()
    {
        playerInputData.Player.Move.Disable();
        playerInputData.Player.Move.performed -= OnMove;
        playerInputData.Player.Move.canceled -= OnMove;
        playerInputData.Player.Fire.started -= OnFire;
        playerInputData.Player.Fire.canceled -= OnFire;
        playerInputData.Player.Jump.started -= OnJump;
        playerInputData.Player.Pause.started -= OnPause;
        playerInputData.Player.Dash.started -= OnDash;
        playerInputData.Player.Dash.canceled -= OnDash;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(m_moveInput.magnitude >= 0)) m_moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        if (isGrounded)
        {
            didDash = false;
        }
        playerAnimController.SetBool(isGroundedHash, isGrounded);
        m_velocityVector = (m_moveInput.x * GetCameraRight(playerCamera) + m_moveInput.z * GetCameraForward(playerCamera)) * m_fMoveSpeed * Time.deltaTime;
        //rb.AddForce(m_ForceVector, ForceMode.Impulse);
        rb.MovePosition(rb.position + m_velocityVector * m_fMoveSpeed * Time.deltaTime);
        //m_velocityVector = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera pCam)
    {
        Vector3 forward = pCam.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera pCam)
    {
        Vector3 right = pCam.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        Vector2 velocity = obj.ReadValue<Vector2>();
        m_moveInput = new Vector3(velocity.x, 0, velocity.y);
        playerAnimController.SetFloat(movementZHash, velocity.y);
        playerAnimController.SetFloat(movementXHash, velocity.x);
    }
    private void OnFire(InputAction.CallbackContext obj)
    {
        float fired = obj.ReadValue<float>();
        if (fired > 0)
        {
            firing = true;
            playerAnimController.SetTrigger(didFireHash);
        }
        else
        {
            firing = false;
        }
        Debug.Log(firing);
        //playerAnimController.SetBool(isFiringHash, firing);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();

            rb.AddForce((Vector3.up * m_fVerticalJumpForce), ForceMode.Impulse);
            playerAnimController.SetTrigger(isJumpingHash);
        }
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        if (!isGrounded && !didDash && m_moveInput.magnitude >= 0)
        {
            didDash = true;
            audioSource.clip = audioClips[1];
            audioSource.Play();
            rb.velocity = Vector3.zero;
            rb.AddForce(m_velocityVector * m_fDashForce + (Vector3.up * 10.0f), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Raycasts a line down to check if the player has touched the ground
    /// </summary>
    private bool CheckGrounded()
    {
        Vector3 CheckPosition = checkGroundRay.position;
        bool groundCheck = Physics.CheckSphere(CheckPosition, m_fGroundedRadius, GroundedLayers, QueryTriggerInteraction.Ignore);

        return groundCheck;
    }
    public void PauseButtonFunction()
    {
        pause = !pause;
       // PausePanel.SetActive(pause);
        AppEvents.InvokeOnPauseEvent(pause);
    }

    public void OnPause(InputAction.CallbackContext obj)
    {
        PauseButtonFunction();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkGroundRay.position, m_fGroundedRadius);
    }
}
