using UnityEngine;


// Класс, содержащий кость направления. Позволяет ее плавно поворачивать
public class DirectionBone : MonoBehaviour
{
    [SerializeField] private Transform _bone;
    public Vector2 bonePosition { get => _bone.position; }
    [HideInInspector] public Vector2 direction;
    public Vector2 currentDirection { get => _bone.right; }

    private const float turningSpeed = 10f;

    private void Update()
    {
        _bone.right = Vector2.Lerp(_bone.right, direction, turningSpeed * Time.deltaTime);
    }



}
