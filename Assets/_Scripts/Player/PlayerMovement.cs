using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private Player _player;
    [SerializeField] private float _movementSpeed = 7.0f;
    [SerializeField] private float _jumpForce = 10f;
    [Tooltip("This is how much a fraction of the default-Grounded movement speed will be the movement speed while on air." +
        " A value of 0 will make the player unable to walk while on air. A value of 1 will make him have the same walk speed on air" +
        "as the speed that has while grounded. In between values will give him a fraction of default speed, for instance 0.5 gives him" +
        "half the speed.")]
    [Range(0f, 1f)]
    [SerializeField] private float _walkOnAirForceFraction;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private Transform[] _groundChecks;
    [SerializeField] private LayerMask _groundCheckLayerMask;
    [SerializeField] private BoxCollider2D _playerCollider;
    [SerializeField] private CameraControl _cameraControl;
    [SerializeField] private AudioSource _playerSoundsAudioSource;
    [SerializeField] private ParticleSystem _landingParticleSystem;
    [SerializeField] private float _initialJumpForce;
    [SerializeField] private float _fallingGravityScale;

    [Header("Player sounds")]
    [SerializeField] private AudioClip _playerJumpAudioClip;
    [SerializeField] private AudioClip _playerLandingAudioClip;
    private Bounds _screenBounds;
    private Camera _mainCam;
    private bool _isGrounded = false;

    private bool _nearMin;
    private bool _nearMax;

    private bool _isPushingScreenBounds = false;

    // Properties
    public bool IsPushingScreenBounds => _isPushingScreenBounds;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        MovePlayer();

    }

    private void Update()
    {

        _screenBounds = OrthographicBounds(_mainCam);

        float buffer = 0.001f;

        // Calculate normalized positions for left and right boundaries
        float normalizedMinX = (_playerCollider.bounds.min.x - _screenBounds.min.x) / _screenBounds.size.x;
        float normalizedMaxX = (_playerCollider.bounds.max.x - _screenBounds.min.x) / _screenBounds.size.x;

        // Check if the player is near the screen bounds with the buffer
        _nearMin = normalizedMinX <= buffer;
        _nearMax = normalizedMaxX >= 1f - buffer;

        _isPushingScreenBounds = _nearMin || _nearMax;

        _isGrounded = CheckGrounded();

        if (_player.PlayerInput.PressedJumpButton && _isGrounded)
        {
            _playerSoundsAudioSource.PlayOneShot(_playerJumpAudioClip);
            ExecuteJumpLogic(_initialJumpForce);
        }

        if (_rb2D.velocity.y < -0.1f)
        {
            _rb2D.gravityScale = _fallingGravityScale;
        }
        else
        {
            _rb2D.gravityScale = 1;
        }
    }

    private void MovePlayer()
    {
         // That means that the player is near the left screen bounds and pushing towards them
         // so we need to block his movement
        if (_nearMin && _player.PlayerInput.HorizontalInput < 0)
        {
            Vector3 newPosition = new Vector3(_screenBounds.min.x + 0.5f, transform.position.y, _playerCollider.bounds.center.z);
            transform.position = newPosition;
            _player.PlayerAnimation.ChangeState(2);
            
            return;
        }
        // Same for the right bounds
        else if (_nearMax && _player.PlayerInput.HorizontalInput > 0)
        {
            Vector3 newPosition = new Vector3(_screenBounds.max.x - 0.5f, transform.position.y, _playerCollider.bounds.center.z);
            transform.position = newPosition;
            _player.PlayerAnimation.ChangeState(2);
            return;
        }
        else if (_player.PlayerInput.HorizontalInput == 0)
        {
            _player.PlayerAnimation.ChangeState(0);
            StopPlayer();
        }

        // Walk left-right default only if grounded or player touching rock
        if ((_isGrounded || _player.PlayerPhysics.IsTouchingRock) && !(IsPushingScreenBounds) && _player.PlayerInput.HorizontalInput != 0)
        {
            _player.PlayerAnimation.ChangeState(1);
            if (_player.PlayerPhysics.IsPushingRock)
            {
                _player.PlayerAnimation.ChangeState(2);
            }

            Vector2 force = new Vector2(_player.PlayerInput.HorizontalInput * _movementSpeed, 0f);
            _rb2D.AddForce(force, ForceMode2D.Force);
        }
        else if (!_isGrounded && !_isPushingScreenBounds)
        {
            Vector2 horizontalForce = new Vector2(_player.PlayerInput.HorizontalInput * _movementSpeed * _walkOnAirForceFraction, 0f);
            _rb2D.AddForce(horizontalForce, ForceMode2D.Force);
        }

        if (_cameraControl.OrthographicSize >= _cameraControl.MaxOrthoSize - 0.1f && _isPushingScreenBounds)
        {
            Vector2 screenCenter = _mainCam.transform.position;
            if (transform.position.x > screenCenter.x)
            {
                // Player is on the right side of the screen
                Vector3 newPosition = new Vector3(_screenBounds.max.x - 0.8f, transform.position.y, _playerCollider.bounds.center.z);
                transform.position = newPosition;
                return;
            }
            else
            {
                // Player is on the left side of the screen
                Vector3 newPosition = new Vector3(_screenBounds.min.x + 0.8f, transform.position.y, _playerCollider.bounds.center.z);
                transform.position = newPosition;
                return;
            }
        }

       
    }

    private void StopPlayer()
    {
        // Stop player from moving but retain velocity.y in case of jumping
        _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y);
    }

    private void ExecuteJumpLogic(float jumpForce)
    {
        
        _rb2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool CheckGrounded()
    {
        foreach (Transform groundCheck in _groundChecks)
        {
            Ray ray = new Ray(groundCheck.position, Vector3.down);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.1f, _groundCheckLayerMask);

            if (hit.collider != null)
            {
                if (!_isGrounded)
                {
                    _playerSoundsAudioSource.clip = _playerLandingAudioClip;
                    _playerSoundsAudioSource.PlayDelayed(0.05f);
                    _landingParticleSystem.Play();
                }
                return true;
            }
        }

        return false;
    }

    public static Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
