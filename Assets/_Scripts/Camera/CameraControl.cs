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

    private void Start()
    {
        // Set the initial orthographic size
        _cmCam.m_Lens.OrthographicSize = _maxOrthoSize;
        _cmFramingTransposer = _cmCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        // Switch to primary camera if pushing rock
        bool shouldFollowPlayer = (_player.PlayerPhysics.IsPushingRock);

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
        if (_player.PlayerPhysics.IsPushingRock && _cmCam.m_Lens.OrthographicSize > _minOrthoSize)
        {
            _cmCam.m_Lens.OrthographicSize -= _zoomSpeed * Time.deltaTime;
        }

        // Zoom out
        else if (_player.PlayerMovement.IsPushingLeftBounds && _cmCam.m_Lens.OrthographicSize < _maxOrthoSize)
        {
            _cmCam.m_Lens.OrthographicSize += _zoomSpeed * Time.deltaTime;
        }
    }

    public void WidenDeadZone()
    {
        _cmFramingTransposer.m_DeadZoneWidth = 2;
    }

    public void SetDeadZoneWidthToZero()
    {
        _cmFramingTransposer.m_DeadZoneWidth = 0;
    }
}