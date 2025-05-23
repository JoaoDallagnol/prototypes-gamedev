using UnityEngine;

public class Staff : MonoBehaviour, IWeapon {

    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnimator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        MouseFollowWithOffSet();
    }
    public void Attack() {
        myAnimator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffProjectileAnimEvent() {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
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
