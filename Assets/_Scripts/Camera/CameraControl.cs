using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private CinemachineBrain _cmBrain;

    [SerializeField] private CinemachineVirtualCamera _cmCam;
    private CinemachineFramingTransposer _cmFramingTransposer;

    [SerializeField] private float _minOrthoSize = 1f;
    [SerializeField] private float _maxOrthoSize = 10f;


    [SerializeField] private float _zoomSpeed = 1f;
    [Header("Broadcast on Event Channels")]
    [SerializeField] private VoidEventChannelSO _gameOverSO;

    private bool _isGameOver;

    public float OrthographicSize => _cmCam.m_Lens.OrthographicSize;
    public float MaxOrthoSize => _maxOrthoSize;
    private void Start()
    {
        // Set the initial orthographic size
        _cmCam.m_Lens.OrthographicSize = _maxOrthoSize - 0.12f;
        _cmFramingTransposer = _cmCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        bool shouldFollowPlayer = (_player.PlayerPhysics.IsTouchingRock);

        ExecuteFollowCameraLogic(shouldFollowPlayer);

        ExecuteZoomLogic();
    }

    private void ExecuteFollowCameraLogic(bool shouldFollowPlayer)
    {
        if (shouldFollowPlayer)
        {
            SetDeadZoneWidthToZero();
        }
        else if (!shouldFollowPlayer)
        {
            WidenDeadZone();
        }
    }

    private void ExecuteZoomLogic()
    {
        // Zoom in
        if (_player.PlayerPhysics.IsTouchingRock && _cmCam.m_Lens.OrthographicSize > _minOrthoSize)
        {
            _cmCam.m_Lens.OrthographicSize -= _zoomSpeed * Time.deltaTime;
        }
        // Zoom out
        else if (_player.PlayerMovement.IsPushingScreenBounds && _cmCam.m_Lens.OrthographicSize < _maxOrthoSize)
        {
            _cmCam.m_Lens.OrthographicSize += _zoomSpeed * Time.deltaTime;
        }

        // TODO: Put this in another script
        // Game over check
        if (_cmCam.m_Lens.OrthographicSize <= _minOrthoSize + 0.1f && !_isGameOver)
        {
            _isGameOver = true;
            _gameOverSO.RaiseEvent();
            this.enabled = false;
        }
    }

    public void WidenDeadZone()
    {
        _cmCam.Follow = null;
        _cmFramingTransposer.m_DeadZoneWidth = 2;
    }

    public void SetDeadZoneWidthToZero()
    {
        _cmCam.Follow = _player.gameObject.transform;
        _cmFramingTransposer.m_DeadZoneWidth = 0;
    }
}