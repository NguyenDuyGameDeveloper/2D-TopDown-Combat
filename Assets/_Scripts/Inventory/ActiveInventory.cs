using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void Start()
    {
        playerControls.Inventory.Keybroad.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHighlight(0);
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void ToggleActiveSlot(int _numberValue)
    {
        if (_numberValue == 3 && _numberValue == 4) return;
        ToggleActiveHighlight(_numberValue - 1);
    }
    private void ToggleActiveHighlight(int _indexValue)
    {
        activeSlotIndexNum = _indexValue;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(_indexValue).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        SO_WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.
            Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.SetParent(ActiveWeapon.Instance.transform, false);

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
