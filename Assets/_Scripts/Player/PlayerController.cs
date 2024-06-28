using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }
    #region Info
    private PlayerControls playerControls;

    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private float startingMoveSpeed;
    private Vector2 playerMovement;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private bool facingLeft = false;
    private bool isDashing = false;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = playerMoveSpeed;
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
    public Transform GetWeaponCollider() => weaponCollider;
    public Transform GetSlashAnimSpawnPoint() => slashAnimSpawnPoint;
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
        {
            sr.flipX = true;
            facingLeft = true;
        }
        else
        {
            sr.flipX = false;
            facingLeft = false;
        }
    }
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            playerMoveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        playerMoveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
