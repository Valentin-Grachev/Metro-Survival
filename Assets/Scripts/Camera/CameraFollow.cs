using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance { get; private set; }

    private Vector2 _newLerpPosition;

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    



    private void Update()
    {
        float newPosX = Mathf.Lerp(transform.position.x, _newLerpPosition.x, Time.deltaTime * Constants.smoothCamera);
        float newPosY = Mathf.Lerp(transform.position.y, _newLerpPosition.y, Time.deltaTime * Constants.smoothCamera);
        transform.position = new Vector3(newPosX, newPosY, transform.position.z);

    }
     

    public void SmoothMove(Vector2 position) => _newLerpPosition = position;

}
