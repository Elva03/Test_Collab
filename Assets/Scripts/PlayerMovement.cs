using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.Serialization;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed = 4f;
    public float jumpSpeed = 10f;
    private Vector2 _desiredVelocity;

    [Header("CoyoteTime")] 
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    [Header("JumpBuffer")] 
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    
    [Header("Acceleration")]
    public float accelerationTime = 0.02f;
    public float groundFriction = 0.03f;
    public float airFriction = 0.005f;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Audio")]
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private PlayerInput _input;
    //private PlayerAnimator _animation;
    //private AudioSource _audioSource;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        //_animation = GetComponent<PlayerAnimation>();
        //_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        _desiredVelocity = _rigidbody2D.velocity;

        //_animation.UpdateAnimation(_desiredVelocity, IsPlayerGrounded(), _input.moveDirection);

        if (IsPlayerGrounded())
        { coyoteTimeCounter = coyoteTime; }
        else
        { coyoteTimeCounter -= 1 * Time.deltaTime; }

        if (_input.jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= 1 * Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            _desiredVelocity.y = jumpSpeed;
            jumpBufferCounter = 0f;
        }

        if (_input.jumpReleased && _rigidbody2D.velocity.y > 0f)
        {
            _desiredVelocity.y *= 0.5f;
            coyoteTimeCounter = 0f;
        }
        _rigidbody2D.velocity = _desiredVelocity;
    }

    private void FixedUpdate()
    {
        if (_input.moveDirection.x != 0)
        {
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x,
                moveSpeed * _input.moveDirection.x,
                accelerationTime);
        }
        else
        {
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 0f, 
                IsPlayerGrounded() ? groundFriction : airFriction);
        }

        _rigidbody2D.velocity = _desiredVelocity;
    }

    private bool IsPlayerGrounded()
    {
        return Physics2D.Raycast(transform.position,
            Vector2.down,
            1.6f,
            whatIsGround);
    }
}
