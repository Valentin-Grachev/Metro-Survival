using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [HideInInspector] public Team team;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] protected bool collideInRoadCenter = false;

    protected Animator _animator;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        // ���� ������ ������� � ����������� ����� ��������
        transform.right = rb.velocity;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        // ��� ��������� � ����������� ���� ����������
        if (collider.CompareTag("Obstacle") ||
            (collider.CompareTag("RoadCenter") && collideInRoadCenter))
        {
            _animator.SetTrigger("Collision");
            rb.velocity = Vector3.zero;
        }
    }




}
