using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Info
    private PlayerControls playerControls;

    private float playerMoveSpeed = 4f;
    private Vector2 playerMovement;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    #endregion

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void PlayerInput()
    {
        playerMovement = playerControls.Movement.Move.ReadValue<Vector2>();

        anim.SetFloat("moveX", playerMovement.x);
        anim.SetFloat("moveY", playerMovement.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position + playerMovement * (playerMoveSpeed * Time.fixedDeltaTime));
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerPos.x)
            sr.flipX = true;
        else
            sr.flipX = false;
    }
}
