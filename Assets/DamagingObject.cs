using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamagingObject : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        DealDamage(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other.gameObject);
    }

    private void DealDamage(GameObject other)
    {
        var heroKnight = other.GetComponent<HeroKnight>();
        if (heroKnight != null)
        {
            heroKnight.DealDamage(damageAmount);
        }
    }

}