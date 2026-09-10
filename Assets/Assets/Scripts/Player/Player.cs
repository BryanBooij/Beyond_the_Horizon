using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private float topBound;
    private float bottomBound;
    private float leftBound;
    private float rightBound;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        // screen boundaries for player ship so it cannot move outside the screen
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;
        Collider2D playerCollider = GetComponent<Collider2D>();
        float halfWidth = playerCollider.bounds.extents.x;
        float halfHeight = playerCollider.bounds.extents.y;
        topBound = cam.transform.position.y + cameraHeight - halfHeight;
        bottomBound = cam.transform.position.y - cameraHeight + halfHeight;
        leftBound = cam.transform.position.x - cameraWidth + halfWidth;
        rightBound = cam.transform.position.x - halfWidth;
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        Vector2 keyboardInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) keyboardInput.y = 1f;
            if (Keyboard.current.sKey.isPressed) keyboardInput.y = -1f;
            if (Keyboard.current.dKey.isPressed) keyboardInput.x = 1f;
            if (Keyboard.current.aKey.isPressed) keyboardInput.x = -1f;
        }

        Vector2 stickInput = Vector2.zero;
        if (Gamepad.current != null)
        {
            stickInput = Gamepad.current.leftStick.ReadValue();
        }

        Vector2 combined = keyboardInput + stickInput;
        moveInput = combined.magnitude > 1f ? combined.normalized : combined;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector2 clampedPos = rb.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, leftBound, rightBound);
        clampedPos.y = Mathf.Clamp(clampedPos.y, bottomBound, topBound);

        rb.position = clampedPos;
    }
}