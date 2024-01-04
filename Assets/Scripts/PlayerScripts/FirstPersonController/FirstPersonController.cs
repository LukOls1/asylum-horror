// CHANGE LOG
// 
// CHANGES || version VERSION
//
// "Enable/Disable Headbob, Changed look rotations - should result in reduced camera jitters" || version 1.0.1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private PickingItems pickingItems;

    #region Camera Movement Variables

    public Camera PlayerCamera;

    public float Fov = 60f;
    public bool InvertCamera = false;
    public bool CameraCanMove = true;
    public float MouseSensitivity = 2f;
    public float MaxLookAngle = 50f;

    // Crosshair
    public bool LockCursor = true;
    public bool Crosshair = true;
    public Sprite CrosshairImage;
    public Color CrosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image CrosshairObject;

    #region Camera Zoom Variables

    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFov = 30f;
    public float zoomStepTime = 5f;

    // Internal Variables
    private bool isZoomed = false;

    #endregion
    #endregion

    #region Movement Variables

    public bool PlayerCanMove = true;
    public float WalkSpeed = 5f;
    public float OriginalWalkSpeed;
    public float MaxVelocityChange = 10f;

    // Internal Variables
    private bool IsWalking = false;

    #region Sprint

    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFov = 80f;
    public float sprintFovStepTime = 10f;

    // Sprint Bar
    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;
    public Image sprintBarBG;
    public Image sprintBar;
    public float sprintBarWidthPercent = .3f;
    public float sprintBarHeightPercent = .015f;

    // Internal Variables
    private CanvasGroup sprintBarCG;
    private bool isSprinting = false;
    private float sprintRemaining;
    private float sprintBarWidth;
    private float sprintBarHeight;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    #endregion

    #region Jump

    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    // Internal Variables
    private bool isGrounded = false;

    #endregion

    #region Crouch

    public bool EnableCrouch = true;
    public bool HoldToCrouch = true;
    public KeyCode CrouchKey = KeyCode.LeftControl;
    public float CrouchHeight = .75f;
    public float SpeedReduction = .5f;
    public bool IsCrouched = false;

    // Internal Variables
    private float originalColliderRadius;
    private Vector3 originalScale;

    #endregion
    #endregion

    #region Head Bob

    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion

    #region Hide

    public bool IsHidden = false;
    public Vector3 lastPlayerPosition;
    private float hiddenColliderRadius = 0.15f;
    private float hiddenScaleBed = 0.1f;
    private float hiddenScaleDesk = 0.35f;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        pickingItems = GetComponent<PickingItems>();

        CrosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        OriginalWalkSpeed = WalkSpeed;
        PlayerCamera.fieldOfView = Fov;
        originalColliderRadius = capsuleCollider.radius;
        originalScale = transform.localScale;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        if(LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Crosshair)
        {
            CrosshairObject.sprite = CrosshairImage;
            CrosshairObject.color = CrosshairColor;
        }
        else
        {
            CrosshairObject.gameObject.SetActive(false);
        }
    }

    float camRotation;

    private void Update()
    {
        #region Camera

        // Control camera movement
        if(CameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * MouseSensitivity;

            if (!InvertCamera)
            {
                pitch -= MouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += MouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -MaxLookAngle, MaxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            PlayerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #region Camera Zoom

        if (enableZoom)
        {
            // Changes isZoomed when key is pressed
            // Behavior for toogle zoom
            if(Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
            {
                if (!isZoomed)
                {
                    isZoomed = true;
                }
                else
                {
                    isZoomed = false;
                }
            }

            // Changes isZoomed when key is pressed
            // Behavior for hold to zoom
            if(holdToZoom && !isSprinting)
            {
                if(Input.GetKeyDown(zoomKey))
                {
                    isZoomed = true;
                }
                else if(Input.GetKeyUp(zoomKey))
                {
                    isZoomed = false;
                }
            }

            // Lerps camera.fieldOfView to allow for a smooth transistion
            if(isZoomed)
            {
                PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, zoomFov, zoomStepTime * Time.deltaTime);
            }
            else if(!isZoomed && !isSprinting)
            {
                PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, Fov, zoomStepTime * Time.deltaTime);
            }
        }

        #endregion
        #endregion

        #region Sprint

        if(enableSprint)
        {
            if(isSprinting)
            {
                isZoomed = false;
                PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, sprintFov, sprintFovStepTime * Time.deltaTime);

                // Drain sprint remaining while sprinting
                if(!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                // Regain sprint while not sprinting
                sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            }

            // Handles sprint cooldown 
            // When sprint remaining == 0 stops sprint ability until hitting cooldown
            if(isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }

            // Handles sprintBar 
            if(useSprintBar && !unlimitedSprint)
            {
                float sprintRemainingPercent = sprintRemaining / sprintDuration;
                sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);
            }
        }

        #endregion

        #region Jump

        // Gets input and calls jump method
        if(enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion

        #region Crouch

        if (EnableCrouch)
        {
            if(Input.GetKeyDown(CrouchKey) && !HoldToCrouch)
            {
                Crouch();
            }
            
            if(Input.GetKeyDown(CrouchKey) && HoldToCrouch)
            {
                IsCrouched = false;
                Crouch();
            }
            else if(Input.GetKeyUp(CrouchKey) && HoldToCrouch)
            {
                IsCrouched = true;
                Crouch();
            }
        }

        #endregion

        CheckGround();

        if(enableHeadBob)
        {
            HeadBob();
        }
    }

    void FixedUpdate()
    {
        #region Movement
        if (!PlayerCanMove)
        {
            Vector3 targetVelocity = new Vector3(0, 0, 0);
            enableHeadBob = false;
        }
        else if (PlayerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Checks if Player is walking and isGrounded
            // Will allow head bob
            enableHeadBob = true;
            if (targetVelocity.x != 0 || targetVelocity.z != 0 && isGrounded)
            {
                IsWalking = true;
            }
            else
            {
                IsWalking = false;
            }

            // All movement calculations shile sprint is active
            if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
            {
                targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.y = 0;

                // Player is only moving when valocity change != 0
                // Makes sure Fov change only happens during movement
                if (velocityChange.x != 0 || velocityChange.z != 0)
                {
                    isSprinting = true;

                    if (IsCrouched)
                    {
                        Crouch();
                    }

                    if (hideBarWhenFull && !unlimitedSprint)
                    {
                        sprintBarCG.alpha += 5 * Time.deltaTime;
                    }
                }

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            // All movement calculations while walking
            else
            {
                isSprinting = false;

                targetVelocity = transform.TransformDirection(targetVelocity) * WalkSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.y = 0;

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        #endregion
    }

    // Sets isGrounded based on a raycast sent straigth down from the Player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        // Adds force to the Player rigidbody to jump
        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }

        // When crouched and using toggle system, will uncrouch for a jump
        if(IsCrouched && !HoldToCrouch)
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        // Stands Player up to full height
        // Brings WalkSpeed back up to original speed
        if(IsCrouched)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            WalkSpeed /= SpeedReduction;

            IsCrouched = false;
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, CrouchHeight, originalScale.z);
            WalkSpeed *= SpeedReduction;

            IsCrouched = true;
        }
    }
    private void HeadBob()
    {
        if(IsWalking)
        {
            // Calculates HeadBob speed during sprint
            if(isSprinting)
            {
                timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            }
            // Calculates HeadBob speed during crouched movement
            else if (IsCrouched)
            {
                timer += Time.deltaTime * (bobSpeed * SpeedReduction);
            }
            // Calculates HeadBob speed during walking
            else
            {
                timer += Time.deltaTime * bobSpeed;
            }
            // Applies HeadBob movement
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            // Resets when play stops moving
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }

    public void Hide(HidingField hideable)
    {
        if (!IsHidden)
        {
            ItemSetActive();
            rb.velocity = Vector3.zero;
            lastPlayerPosition = transform.position;
            HidingSwitchPlayerAtributes(hideable);
            HidingSwitchPlayerStates();
            HidingPositionAndRotation(hideable);       
        }
        else if (IsHidden)
        {
            ItemSetActive();
            HidingSwitchPlayerAtributes(hideable);
            HidingSwitchPlayerStates();
            UnHidePosition();
            ResetSpeedIfCrouched();
        }
    }
    private void ItemSetActive()
    {
        if (pickingItems.activeItem != null)
        {
            pickingItems.activeItem.SetActive(!pickingItems.activeItem.activeSelf);
        }
    }
    private void HidingPositionAndRotation(HidingField hideable)
    {
        pitch = 0f;
        PlayerCamera.transform.rotation = hideable.transform.rotation;
        transform.position = hideable.transform.position;
        transform.rotation = hideable.transform.rotation;
    }
    private void UnHidePosition()
    {
        transform.position = lastPlayerPosition;
    }
    private void HidingSwitchPlayerStates()
    {
        if(!IsHidden)
        {
            IsHidden = true;
            EnableCrouch = false;
            PlayerCanMove = false;
        }
        else if (IsHidden)
        {
            IsHidden = false;
            EnableCrouch = true;
            PlayerCanMove = true;
        }
    }
    private void HidingSwitchPlayerAtributes(HidingField hideable)
    {
        if(!IsHidden)
        {
            capsuleCollider.radius = hiddenColliderRadius;
            if (hideable.furnitureType == HidingField.FurnitureType.Bed)
            {
                transform.localScale = new Vector3(originalScale.x, hiddenScaleBed, originalScale.z);
            }
            else if(hideable.furnitureType == HidingField.FurnitureType.Desk)
            {
                transform.localScale = new Vector3(originalScale.x, hiddenScaleDesk, originalScale.z);
            }
        }
        else if (IsHidden)
        {
            OriginalPlayerAtributes();
        }
    }
    public void ResetSpeedIfCrouched()
    {
        if (IsCrouched)
        {
            WalkSpeed /= SpeedReduction;
            IsCrouched = false;
        }
    }
    public void OriginalPlayerAtributes()
    {
        transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        capsuleCollider.radius = originalColliderRadius;
    }
    public void ResetCameraRotation()
    {
        yaw = 0f;
        pitch = 0f;
    }
}



// Custom Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(FirstPersonController)), InitializeOnLoadAttribute]
    public class FirstPersonControllerEditor : Editor
    {
    FirstPersonController fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (FirstPersonController)target;
        SerFPC = new SerializedObject(fpc);
    }

    public override void OnInspectorGUI()
    {
        SerFPC.Update();

        EditorGUILayout.Space();
        GUILayout.Label("Modular First Person Controller", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
        GUILayout.Label("By Jess Case", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        GUILayout.Label("version 1.0.1", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        EditorGUILayout.Space();

        #region Camera Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Camera Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.PlayerCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Camera attached to the controller."), fpc.PlayerCamera, typeof(Camera), true);
        fpc.Fov = EditorGUILayout.Slider(new GUIContent("Field of View", "The camera’s view Angle. Changes the Player camera directly."), fpc.Fov, fpc.zoomFov, 179f);
        fpc.CameraCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Camera Rotation", "Determines if the camera is allowed to move."), fpc.CameraCanMove);

        GUI.enabled = fpc.CameraCanMove;
        fpc.InvertCamera = EditorGUILayout.ToggleLeft(new GUIContent("Invert Camera Rotation", "Inverts the up and down movement of the camera."), fpc.InvertCamera);
        fpc.MouseSensitivity = EditorGUILayout.Slider(new GUIContent("Look Sensitivity", "Determines how sensitive the mouse movement is."), fpc.MouseSensitivity, .1f, 10f);
        fpc.MaxLookAngle = EditorGUILayout.Slider(new GUIContent("Max Look Angle", "Determines the max and min Angle the Player camera is able to look."), fpc.MaxLookAngle, 40, 90);
        GUI.enabled = true;

        fpc.LockCursor = EditorGUILayout.ToggleLeft(new GUIContent("Lock and Hide Cursor", "Turns off the cursor visibility and locks it to the middle of the screen."), fpc.LockCursor);

        fpc.Crosshair = EditorGUILayout.ToggleLeft(new GUIContent("Auto Crosshair", "Determines if the basic Crosshair will be turned on, and sets is to the center of the screen."), fpc.Crosshair);

        // Only displays Crosshair options if Crosshair is enabled
        if(fpc.Crosshair) 
        { 
            EditorGUI.indentLevel++; 
            EditorGUILayout.BeginHorizontal(); 
            EditorGUILayout.PrefixLabel(new GUIContent("Crosshair Image", "Sprite to use as the Crosshair.")); 
            fpc.CrosshairImage = (Sprite)EditorGUILayout.ObjectField(fpc.CrosshairImage, typeof(Sprite), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fpc.CrosshairColor = EditorGUILayout.ColorField(new GUIContent("Crosshair Color", "Determines the color of the Crosshair."), fpc.CrosshairColor);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--; 
        }

        EditorGUILayout.Space();

        #region Camera Zoom Setup

        GUILayout.Label("Zoom", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

        fpc.enableZoom = EditorGUILayout.ToggleLeft(new GUIContent("Enable Zoom", "Determines if the Player is able to zoom in while playing."), fpc.enableZoom);

        GUI.enabled = fpc.enableZoom;
        fpc.holdToZoom = EditorGUILayout.ToggleLeft(new GUIContent("Hold to Zoom", "Requires the Player to hold the zoom key instead if pressing to zoom and unzoom."), fpc.holdToZoom);
        fpc.zoomKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Zoom Key", "Determines what key is used to zoom."), fpc.zoomKey);
        fpc.zoomFov = EditorGUILayout.Slider(new GUIContent("Zoom Fov", "Determines the field of view the camera zooms to."), fpc.zoomFov, .1f, fpc.Fov);
        fpc.zoomStepTime = EditorGUILayout.Slider(new GUIContent("Step Time", "Determines how fast the Fov transitions while zooming in."), fpc.zoomStepTime, .1f, 10f);
        GUI.enabled = true;

        #endregion

        #endregion

        #region Movement Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Movement Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.PlayerCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Player Movement", "Determines if the Player is allowed to move."), fpc.PlayerCanMove);

        GUI.enabled = fpc.PlayerCanMove;
        fpc.WalkSpeed = EditorGUILayout.Slider(new GUIContent("Walk Speed", "Determines how fast the Player will move while walking."), fpc.WalkSpeed, .1f, fpc.sprintSpeed);
        GUI.enabled = true;

        EditorGUILayout.Space();

        #region Sprint

        GUILayout.Label("Sprint", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

        fpc.enableSprint = EditorGUILayout.ToggleLeft(new GUIContent("Enable Sprint", "Determines if the Player is allowed to sprint."), fpc.enableSprint);

        GUI.enabled = fpc.enableSprint;
        fpc.unlimitedSprint = EditorGUILayout.ToggleLeft(new GUIContent("Unlimited Sprint", "Determines if 'Sprint Duration' is enabled. Turning this on will allow for unlimited sprint."), fpc.unlimitedSprint);
        fpc.sprintKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Sprint Key", "Determines what key is used to sprint."), fpc.sprintKey);
        fpc.sprintSpeed = EditorGUILayout.Slider(new GUIContent("Sprint Speed", "Determines how fast the Player will move while sprinting."), fpc.sprintSpeed, fpc.WalkSpeed, 20f);

        //GUI.enabled = !fpc.unlimitedSprint;
        fpc.sprintDuration = EditorGUILayout.Slider(new GUIContent("Sprint Duration", "Determines how long the Player can sprint while unlimited sprint is disabled."), fpc.sprintDuration, 1f, 20f);
        fpc.sprintCooldown = EditorGUILayout.Slider(new GUIContent("Sprint Cooldown", "Determines how long the recovery time is when the Player runs out of sprint."), fpc.sprintCooldown, .1f, fpc.sprintDuration);
        //GUI.enabled = true;

        fpc.sprintFov = EditorGUILayout.Slider(new GUIContent("Sprint Fov", "Determines the field of view the camera changes to while sprinting."), fpc.sprintFov, fpc.Fov, 179f);
        fpc.sprintFovStepTime = EditorGUILayout.Slider(new GUIContent("Step Time", "Determines how fast the Fov transitions while sprinting."), fpc.sprintFovStepTime, .1f, 20f);

        fpc.useSprintBar = EditorGUILayout.ToggleLeft(new GUIContent("Use Sprint Bar", "Determines if the default sprint bar will appear on screen."), fpc.useSprintBar);

        // Only displays sprint bar options if sprint bar is enabled
        if(fpc.useSprintBar)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            fpc.hideBarWhenFull = EditorGUILayout.ToggleLeft(new GUIContent("Hide Full Bar", "Hides the sprint bar when sprint duration is full, and fades the bar in when sprinting. Disabling this will leave the bar on screen at all times when the sprint bar is enabled."), fpc.hideBarWhenFull);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Bar BG", "Object to be used as sprint bar background."));
            fpc.sprintBarBG = (Image)EditorGUILayout.ObjectField(fpc.sprintBarBG, typeof(Image), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Bar", "Object to be used as sprint bar foreground."));
            fpc.sprintBar = (Image)EditorGUILayout.ObjectField(fpc.sprintBar, typeof(Image), true);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            fpc.sprintBarWidthPercent = EditorGUILayout.Slider(new GUIContent("Bar Width", "Determines the width of the sprint bar."), fpc.sprintBarWidthPercent, .1f, .5f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fpc.sprintBarHeightPercent = EditorGUILayout.Slider(new GUIContent("Bar Height", "Determines the height of the sprint bar."), fpc.sprintBarHeightPercent, .001f, .025f);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }
        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

        #region Jump

        GUILayout.Label("Jump", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

        fpc.enableJump = EditorGUILayout.ToggleLeft(new GUIContent("Enable Jump", "Determines if the Player is allowed to jump."), fpc.enableJump);

        GUI.enabled = fpc.enableJump;
        fpc.jumpKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Jump Key", "Determines what key is used to jump."), fpc.jumpKey);
        fpc.jumpPower = EditorGUILayout.Slider(new GUIContent("Jump Power", "Determines how high the Player will jump."), fpc.jumpPower, .1f, 20f);
        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

        #region Crouch

        GUILayout.Label("Crouch", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

        fpc.EnableCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Enable Crouch", "Determines if the Player is allowed to crouch."), fpc.EnableCrouch);

        GUI.enabled = fpc.EnableCrouch;
        fpc.HoldToCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Hold To Crouch", "Requires the Player to hold the crouch key instead if pressing to crouch and uncrouch."), fpc.HoldToCrouch);
        fpc.CrouchKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Crouch Key", "Determines what key is used to crouch."), fpc.CrouchKey);
        fpc.CrouchHeight = EditorGUILayout.Slider(new GUIContent("Crouch Height", "Determines the y scale of the Player object when crouched."), fpc.CrouchHeight, .1f, 1);
        fpc.SpeedReduction = EditorGUILayout.Slider(new GUIContent("Speed Reduction", "Determines the percent 'Walk Speed' is reduced by. 1 being no reduction, and .5 being half."), fpc.SpeedReduction, .1f, 1);
        GUI.enabled = true;

        #endregion

        #endregion

        #region Head Bob

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Head Bob Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.enableHeadBob = EditorGUILayout.ToggleLeft(new GUIContent("Enable Head Bob", "Determines if the camera will bob while the Player is walking."), fpc.enableHeadBob);
        

        GUI.enabled = fpc.enableHeadBob;
        fpc.joint = (Transform)EditorGUILayout.ObjectField(new GUIContent("Camera Joint", "Joint object position is moved while head bob is active."), fpc.joint, typeof(Transform), true);
        fpc.bobSpeed = EditorGUILayout.Slider(new GUIContent("Speed", "Determines how often a bob rotation is completed."), fpc.bobSpeed, 1, 20);
        fpc.bobAmount = EditorGUILayout.Vector3Field(new GUIContent("Bob Amount", "Determines the amount the joint moves in both directions on every axes."), fpc.bobAmount);
        GUI.enabled = true;

        #endregion

        //Sets any changes from the prefab
        if(GUI.changed)
        {
            EditorUtility.SetDirty(fpc);
            Undo.RecordObject(fpc, "FPC Change");
            SerFPC.ApplyModifiedProperties();
        }
    }

}

#endif