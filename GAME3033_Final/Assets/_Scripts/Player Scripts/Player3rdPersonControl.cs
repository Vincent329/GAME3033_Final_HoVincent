using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player3rdPersonControl : MonoBehaviour
{
    #region --------- Member Variables ------------------
    [SerializeField] private GameObject CinemachineCameraTarget;
    [SerializeField] private float m_fAimSensitivity = 5.0f;
    [SerializeField] private float m_fBottomClamp = -30.0f;
    [SerializeField] private float m_fTopClamp = 70.0f;
    [SerializeField] private float m_fThreshold = 1.0f / 60.0f;
    [SerializeField] private float m_fCameraAngleOverride = 0.0f;
    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform aimLocation;
    public Transform getAimLocation => aimLocation;

    [SerializeField] private WeaponScript weapon;

    //[SerializeField] private Transform playVFXLocation;

    // cinemachine camera values
    float _cinemachineTargetYaw;
    float _cinemachineTargetPitch;
    bool init = false;
    [SerializeField] private Vector2 m_lookVector;

    // reference to attached player movement component
    private PlayerMovement playerMovement;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        CinemachineCameraTarget.transform.rotation = Quaternion.identity;
        init = true;
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.getPlayerInputData.Player.Look.performed += OnLook;
        playerMovement.getPlayerInputData.Player.Look.canceled += OnLook;
                       
        playerMovement.getPlayerInputData.Player.Fire.started += FireWeapon;
    }

    private void OnEnable()
    {
        if (init)
        {
            playerMovement.getPlayerInputData.Player.Look.performed += OnLook;
            playerMovement.getPlayerInputData.Player.Look.canceled += OnLook;

            playerMovement.getPlayerInputData.Player.Fire.started += FireWeapon;
        }
    }
    private void OnDisable()
    {
        if (init)
        {
            playerMovement.getPlayerInputData.Player.Look.performed -= OnLook;
            playerMovement.getPlayerInputData.Player.Look.canceled -= OnLook;

            playerMovement.getPlayerInputData.Player.Fire.started -= FireWeapon;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraRotation();
        AimUpdate();
    }
    void CameraRotation()
    {
        if (m_lookVector.sqrMagnitude >= m_fThreshold)
        {
            _cinemachineTargetYaw += m_lookVector.x * Time.deltaTime * m_fAimSensitivity;
            _cinemachineTargetPitch -= m_lookVector.y * Time.deltaTime * m_fAimSensitivity;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, m_fBottomClamp, m_fTopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + m_fCameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        transform.rotation = Quaternion.Euler(0, CinemachineCameraTarget.transform.rotation.eulerAngles.y, 0);

    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    /// <summary>
    /// updates the raycast
    /// </summary>
    private void AimUpdate()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, aimColliderMask))
        {
            aimLocation.position = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        //if (playerMovement.firing)
        //{
        //    if (hitTransform != null)
        //    {
        //        Debug.Log("Hit");
        //    }
        //}
    }
    private void OnLook(InputAction.CallbackContext obj)
    {
        m_lookVector = obj.ReadValue<Vector2>();
    }
    private void FireWeapon(InputAction.CallbackContext obj)
    {
        if (!playerMovement.pause)
        {
            weapon.FireWeapon(aimLocation);
        }
    }
}
