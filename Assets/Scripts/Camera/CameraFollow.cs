using NTC.Global.Cache;
using UnityEngine;

public class CameraFollow : MonoCache
{
    public static CameraFollow instance { get; private set; }

    [Header("Parameters:")]
    [SerializeField] private float _scaleBattle;
    [SerializeField] private float _scaleMenu;
    [SerializeField] private float _scaleTrolley;
    [SerializeField] private float _scaleCamp;

    [Header("Positions:")]
    [SerializeField] private Transform _leftCameraBorderPositionInBattle;
    [SerializeField] private Transform _menuPosition;
    [SerializeField] private Transform _trolleyPosition;
    [SerializeField] private Transform _campPosition;

    private bool _lerping;
    private Vector2 _lerpPosition;
    
    private bool _scaling;
    private float _cameraScale;

    private Camera _camera;
    private float _normalCameraSize;
    private float _distanceBetweenLeftBorderAndCameraPivot;

    
    


    private const float scalingSpeed = 4f;
    private const float lerpingSpeed = 3f;

    
    


    private void Awake() => instance = this;

    private void Start()
    {
        _lerping = false;
        _scaling = false;
        _camera = Camera.main;
        _normalCameraSize = _camera.orthographicSize;
        _distanceBetweenLeftBorderAndCameraPivot = 
            Mathf.Abs
            (
            _camera.ScreenToWorldPoint(new Vector3(0f, 0f, _camera.farClipPlane)).x -
            _camera.transform.position.x
            );
    }



    public void ToMenu() => SmoothMove(_menuPosition.position, _scaleMenu);

    public void ToTrolley() => SmoothMove(_trolleyPosition.position, _scaleTrolley);

    public void ToCamp() => SmoothMove(_campPosition.position, _scaleCamp);

    public void ToBattle()
    {
        Vector2 resultPosition = _leftCameraBorderPositionInBattle.position;
        resultPosition.x += _distanceBetweenLeftBorderAndCameraPivot;
        SmoothMove(resultPosition, _scaleBattle);

    }




    private void SmoothMove(Vector2 newPosition, float newScale)
    {
        _cameraScale = newScale;
        _scaling = true;

        _lerpPosition = newPosition;
        _lerping = true;
    }



    protected override void Run()
    {
        base.Run();

        if (_scaling)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraScale, scalingSpeed * Time.deltaTime);
            if (_camera.orthographicSize == _cameraScale) _scaling = false;
        }


        if (_lerping)
        {
            Vector3 resultVector = _camera.transform.position;
            resultVector.x = Mathf.Lerp(resultVector.x, _lerpPosition.x, lerpingSpeed * Time.deltaTime);
            resultVector.y = Mathf.Lerp(resultVector.y, _lerpPosition.y, lerpingSpeed * Time.deltaTime);
            resultVector.z = _camera.transform.position.z;
            _camera.transform.position = resultVector;
        }



    }


    



}
