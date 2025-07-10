using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed = 2f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public bool isSprint = false;
    public bool isDoubleJump = false;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool canDoubleJump;

    public GameObject playerSprite;
    public Animator anim;

    private bool isAlive;

    [Header("Coletable")]
    public TextMeshProUGUI scoreT;
    public int score;

    [Header("Audio")]
    public AudioSource aud;
    public AudioClip jump;
    public AudioClip doubleJump;
    public AudioClip addScore;

    [Header("Sipo")]
    public bool isOnSipo = false;
    public float sipoVerticalSpeed = 2f;

    void Start()
    {
        CheckPowers();
        isAlive = true;
        score = 0;
        scoreT.text = score.ToString();
        rb = GetComponent<Rigidbody2D>();
    }

    void CheckPowers()
    {
        if (PlayerPrefs.GetInt("DB", 0) == 1)
        {
            isDoubleJump = true;
        }

        if (PlayerPrefs.GetInt("SP", 0) == 1)
        {
            isSprint = true;
        }
    }

    public void UnlockSprint()
    {
        isSprint = true;
        PlayerPrefs.SetInt("SP", 1);
    }

    public void UnlockDoubleJump()
    {
        isDoubleJump = true;
        PlayerPrefs.SetInt("DB", 1);
    }

    public void SetKilled()
    {
        isAlive = false;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        // Caso esteja no cipó
        if (isOnSipo)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(0f, vertical * sipoVerticalSpeed);

            // Sair do cipó com pulo
            if (Input.GetButtonDown("Jump"))
            {
                isOnSipo = false;
                rb.gravityScale = 3f; // Ajuste conforme seu jogo
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                anim.SetTrigger("Jump");
                aud.PlayOneShot(jump);
            }

            return; // interrompe o resto do update
        }

        // Checa se está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Movimento horizontal com Sprint se ativo e pressionado
        float speed = moveSpeed;
        if (isSprint && Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintMultiplier;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flip do sprite
        if (moveInput >= 0)
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        // Animação de corrida
        anim.SetBool("Run", moveInput != 0);

        // Reseta double jump
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // Pulo e double jump
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", !isGrounded);

            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                aud.PlayOneShot(jump);
            }
            else if (isDoubleJump && canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false;
                anim.SetTrigger("Double Jump");
                aud.PlayOneShot(doubleJump);
            }
        }
    }

    public void AddScore(int aux)
    {
        aud.PlayOneShot(addScore);
        score += aux;
        scoreT.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sipo"))
        {
            isOnSipo = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Sipo"))
        {
            isOnSipo = false;
            rb.gravityScale = 1f; 
           
        }
    }
}
