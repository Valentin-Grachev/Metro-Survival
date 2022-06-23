using UnityEngine;

public class AbilityDestination : MonoBehaviour
{
    public static AbilityDestination instance { get; private set; }
    public delegate void OnDestination(Vector2 destination);
    public event OnDestination onDestination;


    [SerializeField] private GameObject _fade;
    [SerializeField] private Transform _aimPosition; public Vector2 aimPosition { get => _aimPosition.transform.position; }

    [HideInInspector] public Vector2 startPosition;
    [HideInInspector] public float minDistance;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public GameObject aim;

    private GameObject _instAim;

    private const float timeScaleDuringEnable = 0.3f;

    private void Awake()
    {
        if (instance == null) instance = this;
    }




    public void OnEnable()
    {
        _fade.SetActive(true);
        _instAim = Instantiate(aim, _aimPosition);
        Time.timeScale = timeScaleDuringEnable;
        float middleX = startPosition.x + (minDistance + maxDistance)/2f;
        _aimPosition.transform.position = new Vector3(middleX, _aimPosition.transform.position.y, _aimPosition.transform.position.z);
    }

    public void OnDisable()
    {
        _fade.SetActive(false);
        Destroy(_instAim);
        Time.timeScale = 1f;
    }



    private void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (startPosition.x + minDistance > mousePosition.x) mousePosition.x = startPosition.x + minDistance;
            else if (startPosition.x + maxDistance < mousePosition.x) mousePosition.x = startPosition.x + maxDistance;

            _aimPosition.transform.position = new Vector3(mousePosition.x, _aimPosition.transform.position.y, _aimPosition.transform.position.z);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            onDestination.Invoke(_aimPosition.transform.position);
            onDestination = null;
            enabled = false;
        }


    }


}
