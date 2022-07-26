using UnityEngine;

public class BulletLimiter : MonoBehaviour
{
    public static BulletLimiter instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Vector2 _battleArea;
    [SerializeField] private float _detectionLine; public float detectionLine { get => _detectionLine; }

    public bool ObjectIsInsideArea(Vector2 position)
    {
        return transform.position.x - _battleArea.x/2f < position.x && position.x < transform.position.x + _battleArea.x / 2f
            && transform.position.y - _battleArea.y / 2f < position.y && position.y < transform.position.y + _battleArea.y / 2f;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _battleArea);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + new Vector3(_detectionLine, transform.position.y - 10f, transform.position.z),
            transform.position + new Vector3(_detectionLine, transform.position.y + 10f, transform.position.z));
    }

}
