using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private SO_WeaponInfo so_weaponInfo;

    public SO_WeaponInfo GetWeaponInfo() { return so_weaponInfo; }
}
