using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f);

    [Header("Rotation")]
    public float mouseSensitivity = 3f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    [Header("Collision")]
    public LayerMask collisionLayers;
    public float minDistance = 1f;
    public float maxDistance = 5f;
    public float collisionBuffer = 0.2f;

    [Header("Smoothing")]
    public float positionSmoothing = 10f;
    public float rotationSmoothing = 10f;

    float currentX;
    float currentY;
    float currentDistance;
    bool isLocked;

    void Start()
    {
        currentDistance = offset.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleInput();
        HandleCollision();
        UpdatePosition();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLocked = !isLocked;
            Cursor.lockState = isLocked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isLocked;
        }

        if (!isLocked)
        {
            currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
            currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);
        }
    }

    void HandleCollision()
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(currentY, currentX, 0) * offset;
        Vector3 direction = desiredPosition - target.position;
        float desiredDistance = direction.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(target.position, direction.normalized, out hit, desiredDistance, collisionLayers))
        {
            currentDistance = Mathf.Max(hit.distance - collisionBuffer, minDistance);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, positionSmoothing * Time.deltaTime);
        }

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }

    void UpdatePosition()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 newPosition = target.position + rotation * offset.normalized * currentDistance;

        transform.position = Vector3.Lerp(transform.position, newPosition, positionSmoothing * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
    }
}
