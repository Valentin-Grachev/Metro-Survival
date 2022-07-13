using NTC.Global.Cache;
using UnityEngine;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _fill;
    [SerializeField] private SpriteRenderer _background;

    private void Start()
    {
        GetComponentInParent<Minion>().onHealthChanged += OnHealthChanged_Minion;
    }

    protected void OnEnable() => SetAlpha(0f);



    private void OnHealthChanged_Minion(int health, int maxHealth)
    {
        float weight = (float)health / (float)maxHealth;
        _fill.size = new Vector2(weight, _fill.size.y);

        if (health != maxHealth) SetAlpha(1f);

    }

    private void SetAlpha(float alpha)
    {
        Color color = _background.color;
        color.a = alpha;
        _background.color = color;

        color = _fill.color;
        color.a = alpha;
        _fill.color = color;
    }




}
