using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private enum ScaleMode { Scaling, Unscaling, Idle}
    public static CameraFollow instance { get; private set; }

    [Header("Components:")]
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private Collider2D _confinerCollider;

    [Header("Parameters:")]
    [SerializeField] private float _scaledCameraSize;

    [Header("Positions:")]
    [SerializeField] private Transform _battlePosition;
    [SerializeField] private Transform _menuPosition;
    [SerializeField] private Transform _trolleyPosition;
    [SerializeField] private Transform _campPosition;


    private ScaleMode _scaleMode;
    private float _normalCameraSize;

    // —ледующие махинации нужны, чтобы включить конфайнер, при этом резко не дернуть камеру в бою ограничителем

    // —мещение камеры вправо на данную величину («атем спуст€ врем€ она вернетс€ на указанную позицию)
    private const float _startRightCameraOffset = 12f;
    //  амера сначала идет с запасом вправо, затем через данный интервал времени идет на позицию камеры
    private const float waitTime = 2f;

    private const float scalingSpeed = 4f;

    private void Awake() => instance = this;

    private void Start()
    {
        _scaleMode = ScaleMode.Idle;
        _normalCameraSize = _camera.m_Lens.OrthographicSize;
    }



    public void ToMenu()
    {
        transform.position = _menuPosition.position;
        _scaleMode = ScaleMode.Unscaling;
    }

    public void ToTrolley()
    {
        transform.position = _trolleyPosition.position;
        _scaleMode = ScaleMode.Scaling;
    }

    public void ToCamp() => transform.position = _campPosition.position;

    public void ToBattle()
    {
        transform.position = new Vector3(_battlePosition.position.x + _startRightCameraOffset,
            _battlePosition.position.y, transform.position.z);
        StartCoroutine(MoveCameraToPosition());
    }

    IEnumerator MoveCameraToPosition()
    {
        yield return new WaitForSeconds(waitTime);

        // ¬ключение ограничител€ камеры и возврат в камеры в нужное место
        transform.position = new Vector3(_battlePosition.position.x, _battlePosition.position.y, transform.position.z);
        _confiner.m_BoundingShape2D = _confinerCollider;
    }

    private void Update()
    {
        if (_scaleMode == ScaleMode.Scaling)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _scaledCameraSize, scalingSpeed * Time.deltaTime);
            if (_camera.m_Lens.OrthographicSize == _scaledCameraSize) _scaleMode = ScaleMode.Idle;
        }
        else if (_scaleMode == ScaleMode.Unscaling)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _normalCameraSize, scalingSpeed * Time.deltaTime);
            if (_camera.m_Lens.OrthographicSize == _normalCameraSize) _scaleMode = ScaleMode.Idle;
        }


    }



}
