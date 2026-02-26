using UnityEngine;
using TMPro;

public class Weapons : MonoBehaviour
{
    [Header("References")]
    public WeaponData[] weapons;
    public int currentWeaponIndex = 0;
    WeaponData currentWeapon;
    int currentMag;
    float nextTimeToFire;
    int[] ammoCount;
    [SerializeField] ObjectPooler pooler;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce = 20f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI weaponText;

    void Start()
    {
        ammoCount = new int[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            ammoCount[i] = weapons[i].size;
        }

        EquipWeapon(0);
    }

    void EquipWeapon(int index)
    {
        if(index < 0 || index >= weapons.Length)
        {
            return;
        }

        currentWeaponIndex = index;
        currentWeapon = weapons[index];
        currentMag = ammoCount[index];

        Debug.Log("Equipped " + currentWeapon.weaponName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        UIupdate();
    }

    void Shoot()
    {
        if(Time.time < nextTimeToFire)
        {
            return;
        }

        if (currentMag > 0)
        {
            GameObject bullet = pooler.SpawnFromPools(currentWeapon.bulletPrefab.name, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            currentMag--;
            ammoCount[currentWeaponIndex] = currentMag;
            nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
        }
        else
        {
            Debug.Log("Out of ammo for " + currentWeapon.weaponName);
        }
    }

    void UIupdate()
    {
        if(weaponText != null)
        {
            weaponText.text = $"Weapon: {currentWeapon.weaponName} \n" +
                              $"Ammo: {currentMag}/{currentWeapon.size}";
        }
    }
}