using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 30f;
    public Transform playerCamera;
    public InputActionReference lockCursorAction;
    public Transform target;

    public Animator cameraAnimator;

    public float rayDistance = 4f;
    private float xRotation = 0f;
    private Vector2 mouseInput;
    private bool cursorLocked = true;

    private HighlightOnLook currentHighlight;

    void OnEnable()
    {
        if (lockCursorAction != null)
        {
            lockCursorAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (lockCursorAction != null)
        {
            lockCursorAction.action.Disable();
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void Update()
    {
        if (lockCursorAction != null && lockCursorAction.action.WasPressedThisFrame())
        {
            if (cursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }

        if (cursorLocked)
        {
            LookAround();
        }

        CheckHighlight();

    }

    public void OnLook(InputValue data)
    {
        mouseInput = data.Get<Vector2>();
    }

    public void LookAround()
    {
        xRotation -= mouseInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -40f, 60f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity * Time.deltaTime);
        mouseInput = Vector2.zero;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
        mouseInput = Vector2.zero;

        if (target != null)
        {
            LookAt(target);
            if (cameraAnimator != null)
            {
                cameraAnimator.SetBool("IsFocusing", true);
            }
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
        mouseInput = Vector2.zero;

        if (cameraAnimator != null)
        {
            cameraAnimator.SetBool("IsFocusing", false);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ClearTarget(Transform targetTransform)
    {
        if (target == targetTransform)
        {
            target = null;
        }
    }

    void LookAt(Transform newTarget)
    {
        Vector3 direccion = newTarget.position - transform.position;

        Vector3 direccionHorizontal = new Vector3(direccion.x, 0f, direccion.z);
        if (direccionHorizontal.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direccionHorizontal);
        }
        Vector3 direccionLocal = transform.InverseTransformDirection(direccion);
        float anguloVertical = -Mathf.Atan2(direccionLocal.y, direccionLocal.z) * Mathf.Rad2Deg;
        playerCamera.localRotation = Quaternion.Euler(anguloVertical, 0f, 0f);
    }

 
    void CheckHighlight()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        Camera cam = playerCamera.GetComponent<Camera>();
        if (cam == null)
        {
            ClearHighlight();
            return;
        }
        ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            HighlightOnLook newHighlight = hit.collider.GetComponentInParent<HighlightOnLook>();

            if (newHighlight != null)
            {
                if (currentHighlight != newHighlight)
                {
                    if (currentHighlight != null)
                    {
                        currentHighlight.OnLookExit();
                    }
                    newHighlight.OnLookEnter();
                    currentHighlight = newHighlight;
                }
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (currentHighlight != null)
        {
            currentHighlight.OnLookExit();
            currentHighlight = null;
        }
    }
}