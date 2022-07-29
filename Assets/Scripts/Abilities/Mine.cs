using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public Vector2 activationAreaSize;
    [HideInInspector] public Vector2 damageAreaSize;
    [HideInInspector] public Team damagableTeam;
    [SerializeField] protected Vector2 _explotionSize;

    protected bool detected;
    protected SpineAnimation spineAnimation;

    private void Start()
    {
        detected = false;
        spineAnimation = GetComponent<SpineAnimation>();
        spineAnimation.SetAnimation(AnimationType.Ability_active);
    }

    IEnumerator Scanning()
    {
        while (!detected)
        {
            List<DestroyableObject> list = null;
            if (damagableTeam == Team.Enemy) list = AllMinions.instance.enemies;
            else list = AllMinions.instance.allies;

            for (int i = 0; i < list.Count; i++)
            {
                if (Library.ObjectIsInsideArea(list[i].transform.position, transform.position, activationAreaSize))
                {
                    Active();
                    detected = true;
                    break;
                }    
            }

            yield return new WaitForSeconds(0.5f);
        }
        
    }


    public void Explotion()
    {
        transform.localScale = _explotionSize;
        List<DestroyableObject> list = null;
        if (damagableTeam == Team.Enemy) list = AllMinions.instance.enemies;
        else list = AllMinions.instance.allies;

        for (int i = 0; i < list.Count; i++)
        {
            if (Library.ObjectIsInsideArea(list[i].transform.position, transform.position, damageAreaSize))
            {
                list[i].health -= damage;
            }
        }

        CameraControl.instance.ShakeDown();
    }


    private void Active() => spineAnimation.SetAnimation(AnimationType.Death);

    public void Destroy() => Destroy(gameObject);

    public void BeginDetection()
    {
        StartCoroutine(Scanning());
        spineAnimation.SetAnimation(AnimationType.Idle);
    }



}
