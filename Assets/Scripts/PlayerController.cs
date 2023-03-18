using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    public float doubleJumpForce = 10;
    public float playerSpeed = 5;
    public float xBoundRight = 12;
    public float xBoundLeft = 0;
    public bool playerGrounded = true;
    private Rigidbody _playerRB;
    private float playerStartSpeed;
    private float gameStartSpeed;
    private GameManager _gameManager;
    private Animator _anim;
    public ParticleSystem particlesCollision;
    public  ParticleSystem particlesGround;

    AudioSource _source;
    public AudioClip doubleJumpSound;

    private bool _inputEnabled = true;

    public static float _currentSpeed;

    public bool doubleJumpUsed = false;

    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        _playerRB= GetComponent<Rigidbody>();
        _gameManager= FindObjectOfType<GameManager>();
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();

        playerStartSpeed = playerSpeed;
        gameStartSpeed = _gameManager.gameSpeed;
        _currentSpeed = _gameManager.gameSpeed;
        particlesGround.Play();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 restartPos = new Vector3(-3,0,0);
        if (_gameManager.gameRestarted)
        {
            transform.position = restartPos;
            _gameManager.gameRestarted = false;
        } 


        if (_inputEnabled)
        {
            float inputX = Input.GetAxis("Horizontal");
            if (inputX > 0 && transform.position.x < xBoundRight)
            {
                transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
                if (_gameManager.gameSpeed <= _currentSpeed * 1.3) _gameManager.gameSpeed += 0.05f;
            }

            if (inputX < 0 && transform.position.x > xBoundLeft)
            {
                transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
                if (_gameManager.gameSpeed >= _currentSpeed / 3) _gameManager.gameSpeed -= 0.05f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerGrounded)
            {
                playerGrounded = false;
                particlesGround.Stop();
                _playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJumpUsed = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !playerGrounded && !doubleJumpUsed)
            {
                doubleJumpUsed = true;
                _playerRB.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
                _anim.Play("Running_Jump", 3,0f);
                _source.PlayOneShot(doubleJumpSound);
            }

            if (transform.position.x > xBoundRight) transform.position = new Vector3(xBoundRight, transform.position.y, transform.position.z);

            SetAnimation();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerGrounded = true;
            doubleJumpUsed = false;
            playerSpeed = playerStartSpeed;
            if (_gameManager != null) if (!_gameManager.gameOver) particlesGround.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);   
            particlesCollision.Play();
            particlesGround.Stop();
            _inputEnabled = false;
            _gameManager.gameSpeed = 0;
            _anim.SetBool("Death_b", true);
            _anim.SetInteger("DeathType_int", 1);
            _gameManager.gameOver = true;


        }

    }

    void SetAnimation()
    {
        if (_gameManager.gameSpeed <= 14)
        {
            _anim.SetBool("PlayerRunning", false);
            _anim.SetBool("PlayerWalking", true);
            if (!playerGrounded) _anim.SetBool("PlayerJumping", true);
            else _anim.SetBool("PlayerJumping", false);
            particlesGround.Stop();

        }
        else if (_gameManager.gameSpeed > 14)
        {
            _anim.SetBool("PlayerWalking", false);
            _anim.SetBool("PlayerRunning", true);
            if (!playerGrounded) _anim.SetBool("PlayerJumping", true);
            else _anim.SetBool("PlayerJumping", false);

        }

    }
}
