using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    public int GetDamage()
    {
        return damage;
    }
}