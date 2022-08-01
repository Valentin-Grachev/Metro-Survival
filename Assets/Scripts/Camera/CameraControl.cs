using NTC.Global.Cache;
using System.Collections;
using UnityEngine;

public class CameraControl : MonoCache
{
    public static CameraControl instance { get; private set; }

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
    private Vector2 _battlePosition;

    private bool _moving;
    private Vector2 _lerpPosition;
    private Vector2 _currentMovingVelocity;
    

    private bool _scaling;
    private float _cameraScale;

    private Camera _camera;
    private float _distanceBetweenLeftBorderAndCameraPivot;

    


    private const float scalingSpeed = 3f;
    private const float movingSlow = 0.3f;
    private const float shakingSlow = 0.08f;

    private float _movingSlowCoeff;



    private void Awake() => instance = this;

    private void Start()
    {
        _moving = false;
        _scaling = false;
        _camera = Camera.main;
        _distanceBetweenLeftBorderAndCameraPivot = 
            Mathf.Abs
            (
            _camera.ScreenToWorldPoint(new Vector3(0f, 0f, _camera.farClipPlane)).x -
            _camera.transform.position.x
            );
    }


    public void ShakeDown()
    {
        if (GameSettings.instance.cameraShakeEnabled) StartCoroutine(Shake());
    }


    private IEnumerator Shake()
    {
        SmoothMove(_battlePosition - new Vector2(0f, 0.1f), _camera.orthographicSize, shakingSlow);
        yield return new WaitForSeconds(0.15f);
        SmoothMove(_battlePosition + new Vector2(0f, 0.07f), _camera.orthographicSize, shakingSlow);
        yield return new WaitForSeconds(0.15f);
        SmoothMove(_battlePosition, _camera.orthographicSize, shakingSlow);
    }




    public void ToMenu() => SmoothMove(_menuPosition.position, _scaleMenu, movingSlow);

    public void ToTrolley() => SmoothMove(_trolleyPosition.position, _scaleTrolley, movingSlow);

    public void ToCamp() => SmoothMove(_campPosition.position, _scaleCamp, movingSlow);

    public void ToBattle()
    {
        Vector2 resultPosition = _leftCameraBorderPositionInBattle.position;
        resultPosition.x += _distanceBetweenLeftBorderAndCameraPivot;
        SmoothMove(resultPosition, _scaleBattle, movingSlow);
        _battlePosition = resultPosition;

    }




    private void SmoothMove(Vector2 newPosition, float newScale, float movingSlowCoeff)
    {
        _cameraScale = newScale;
        _scaling = true;

        _lerpPosition = newPosition;
        _moving = true;

        _movingSlowCoeff = movingSlowCoeff;
    }



    protected override void Run()
    {
        base.Run();

        if (_scaling)
        {

            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _cameraScale, scalingSpeed * Time.deltaTime);
            if (_camera.orthographicSize == _cameraScale) _scaling = false;
        }


        if (_moving)
        {
            Vector2 resultVector = _camera.transform.position;
            resultVector = Vector2.SmoothDamp(resultVector, _lerpPosition, ref _currentMovingVelocity, _movingSlowCoeff);
            _camera.transform.position = new Vector3(resultVector.x, resultVector.y, _camera.transform.position.z);
        }




    }






    



}
