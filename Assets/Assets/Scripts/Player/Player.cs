using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Screen Bounds")]
    public float topBound = 2.7f;
    public float bottomBound = -2.7f;
    public float leftBound = -6.5f;
    public float rightBound = 0f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.wKey.isPressed) moveY = 1f;
        if (Keyboard.current.sKey.isPressed) moveY = -1f;
        if (Keyboard.current.dKey.isPressed) moveX = 1f;
        if (Keyboard.current.aKey.isPressed) moveX = -1f;

        moveInput = new Vector2(moveX, moveY).normalized;
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