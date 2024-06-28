using UnityEngine;

public class Null : MonoBehaviour, IWeapon
{
    [SerializeField] private SO_WeaponInfo weaponInfo;
    
    public void Attack()
    {
        
    }

    public SO_WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
