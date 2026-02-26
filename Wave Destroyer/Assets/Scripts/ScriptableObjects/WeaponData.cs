using UnityEngine;

[CreateAssetMenu(fileName = " New Weapon", menuName = "Weapaons")]
public class WeaponData : ScriptableObject
{
    public GameObject bulletPrefab;
    public string weaponName;
    public int size;
    public float fireRate;
    public int damage;
}