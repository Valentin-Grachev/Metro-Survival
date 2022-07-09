using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _startFightCameraPosition;
    [SerializeField] private Vector2 _clamp;
    private Vector2 _startPosition;
    private Camera _camera;

    float directionX;
    Vector3 newPosition;

    private void Start()
    {
        _camera = Camera.main;
        newPosition = _camera.transform.position;
        AbilityDestination.instance.onEnableTargeting += OnEnableTargeting_AbilityDestination;
        AbilityDestination.instance.onDisableTargeting += OnDisableTargeting_AbilityDestination;
    }

    private void OnDisableTargeting_AbilityDestination() => enabled = true;

    private void OnEnableTargeting_AbilityDestination() => enabled = false;


    public void StartCamera() => CameraFollow.instance.SmoothMove(_startFightCameraPosition.position);

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        else if (Input.GetMouseButton(0))
        {
            directionX =  (_startPosition.x - _camera.ScreenToWorldPoint(Input.mousePosition).x) * Constants.sensitivity;
            newPosition = CameraFollow.instance.transform.position;
            newPosition.x = Mathf.Clamp(CameraFollow.instance.transform.position.x + directionX, _clamp.x, _clamp.y);
        }

        
        CameraFollow.instance.SmoothMove(newPosition);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector2 from = new Vector2(_clamp.x, 1f);
        Vector2 to = new Vector2(_clamp.y, 1f);
        Gizmos.DrawLine(from, to);
    }

}
