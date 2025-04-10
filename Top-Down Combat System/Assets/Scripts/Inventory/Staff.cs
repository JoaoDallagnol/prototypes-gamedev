using UnityEngine;

public class Staff : MonoBehaviour, IWeapon {

    [SerializeField] private WeaponInfo weaponInfo;

    private void Update() {
        MouseFollowWithOffSet();
    }
    public void Attack() {
        Debug.Log("STAFF ATTACK!");
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    private void MouseFollowWithOffSet() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        float angle = Mathf.Atan2(mousePos.y - playerPosition.y, Mathf.Abs(mousePos.x - playerPosition.x)) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
           ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
