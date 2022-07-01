using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public float activationRadius;
    [HideInInspector] public float damageRadius;
    [HideInInspector] public LayerMask damageLayer;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Scanning());
    }

    IEnumerator Scanning()
    {
        while (true)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, activationRadius, damageLayer);
            if (collider != null) Active();

            yield return new WaitForSeconds(Constants.scanInterval);
        }
        
    }


    public void Explotion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageLayer);
        foreach (var item in colliders)
        {
            item.GetComponent<DestroyableObject>().health -= damage;
        }
        Destroy(gameObject);
    }


    private void Active()
    {
        animator.SetTrigger("Active");
    }    




}
