using UnityEngine;

public class Staff : MonoBehaviour, IWeapon {
    public void Attack() {
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
