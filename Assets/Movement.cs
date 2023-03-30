using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float Jumpforce = 50f;
    public float MaxSlopeAngle = 30f;

    public float GroundCheckRadius = 1f;
    public CoolDownn CoyoteTime;
    public LayerMask GroundLayerMask;

    public PhysicsMaterial2D Default;
    public PhysicsMaterial2D FullFriction;

    public bool IsRunning
    {
        get
        {
            return _IsRunning;
        }
    }
    public bool FlipAnim
    {
        get
        {
            return _FlipAnim;
        }
        set
        {
            _FlipAnim = value;
        }
    }


    // protected variables 
    protected Rigidbody2D _rigidbody;
    protected Vector2 _InputDirection;
    public float _lastSlopAngle;

    public bool _Isgrounded = true;
    protected RaycastHit2D _groundHit;
    protected RaycastHit2D _slopeHit;




    public float _slopeAngle = 0f;
    protected Vector2 _slopeHitNormal = Vector2.zero;

    protected bool _IsRunning = false;
    protected bool _FlipAnim = false;
    protected bool _Isjumping = false;
    protected bool _CanJump = true;

    private HealthPoint _health;
    private bool _disableInput = false;


    public bool _isOnSlope = false;
    public bool _canWalkOnSlope = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _health = GetComponent<HealthPoint>();

        if (_health != null)
        {
            _health.OnHit += Hit;
            _health.OnHitReset += ResetMove;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnHit -= Hit;
            _health.OnHitReset += ResetMove;
        }
    }

    private void ResetMove()
    {
        _disableInput = false;
    }

    private void Hit(GameObject source)
    {
        float pushHorizontal = 0f;

        if (source != null)
        {
            if (source.transform.position.x < transform.position.x)
            {
                pushHorizontal = Jumpforce;
            }
            else
            {
                pushHorizontal = -Jumpforce;
            }
        }

        _rigidbody.velocity = Vector2.zero;

        _rigidbody.velocity = new Vector2(pushHorizontal, Jumpforce);

        _disableInput = true;

    }
    
    void Update()
    {
        HandleInput();   


        if (Input.GetKeyDown(KeyCode.F))
        {
            CoyoteTime.StartCoolDown();
        }
    }
    private void FixedUpdate()
    {
        CheckGround();
        //CheckSlope();
        HandleMovement();
        HandleFlip();
    }
    protected virtual void HandleInput()
    {

    }

    protected virtual void HandleMovement()
    {
        if (_disableInput)
            return;

        if (_rigidbody == null)
            return;

        _rigidbody.velocity = new Vector2(_InputDirection.x * Acceleration, _rigidbody.velocity.y);

        if (_rigidbody.velocity.x == 0)
        {
            _IsRunning = false;
        }
        else
        {
            _IsRunning = true;
        }
    }

    protected virtual void HandleFlip()
    {
        if (_InputDirection.x == 0)
            return;

        if (_InputDirection.x > 0)
        {
            FlipAnim = false;
        }
        else if (_InputDirection.x < 0)
        {
            FlipAnim = true;
        }
    }

    protected virtual void DoJump()
    {
        if (_rigidbody == null)
            return;

        if (!_CanJump)
            return;

        if (CoyoteTime.CurrentProgress == CoolDownn.Progress.Finished)
            return;

        _CanJump = false;
        _Isjumping = true;

        Debug.Log("Jumping");
        bool reallyGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, GroundLayerMask);

        if (reallyGround)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jumpforce);
            Debug.Log("Normal Jump");
        }
        else
        {
            _disableInput = true;

            RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 1, GroundLayerMask);
            RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 1, GroundLayerMask);

            if (left)
            {
                _rigidbody.velocity = new Vector2(Jumpforce, Jumpforce);
                Debug.Log("Right Jump");
            }
            else if (right)
            {
                _rigidbody.velocity = new Vector2(-Jumpforce, Jumpforce);
                Debug.Log("Left Jump");
            }
        }

        CoyoteTime.StopCoolDown();

    }


    protected void CheckSlope()
    {
        _slopeHit = Physics2D.Raycast(transform.position, Vector2.down, 2f, GroundLayerMask);

        if (_slopeHit)
        {
            _slopeHitNormal = _slopeHit.normal;
            _slopeAngle = Vector2.Angle(Vector2.up, _slopeHitNormal);

            if (_slopeAngle != _lastSlopAngle)
            {
                _isOnSlope = true;
            }

            if (_slopeAngle < 1)
            {
                _isOnSlope = false;
            }


            _lastSlopAngle = _slopeAngle;

        }
        if (_slopeAngle > MaxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }
        // Debug.Log(Vector2.Angle(Vector2.up ,_slopeHit.normal));

        if (_isOnSlope && _canWalkOnSlope && _InputDirection.x == 0)
        {
            _rigidbody.sharedMaterial = FullFriction;
        }
        else
        {
            _rigidbody.sharedMaterial = Default;
        }




    }

    protected void CheckGround()
    {
        _Isgrounded = Physics2D.OverlapCircle(transform.position, GroundCheckRadius, GroundLayerMask);

        if (_rigidbody.velocity.y <= 0f)
        {
            _Isjumping = false;
        }

        if (_Isgrounded && !_Isjumping)
        {
            ResetMove();
            _CanJump = true;
            if (CoyoteTime.CurrentProgress != CoolDownn.Progress.Ready)
            {
                CoyoteTime.StopCoolDown();
            }

        }

        if (!_Isgrounded && !_Isjumping && CoyoteTime.CurrentProgress == CoolDownn.Progress.Ready)
            CoyoteTime.StartCoolDown();
    }
}
