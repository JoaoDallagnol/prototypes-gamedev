using UnityEngine;

public class Sword : MonoBehaviour {
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private void Awake() {
        playerController =  GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffSet();
    }

    private void Attack() {
        // fire our sword animation
        
        myAnimator.SetTrigger("Attack");
    }

    private void MouseFollowWithOffSet() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        Vector3 playerPosition = playerController.transform.position;

        float angle = Mathf.Atan2(mousePos.y - playerPosition.y, Mathf.Abs(mousePos.x - playerPosition.x)) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
           activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
