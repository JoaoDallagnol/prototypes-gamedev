using UnityEngine;

public class Bow : MonoBehaviour, IWeapon {

    [SerializeField] private WeaponInfo weaponInfo;
    public void Attack() {
        Debug.Log("BOW ATTACK!");
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
