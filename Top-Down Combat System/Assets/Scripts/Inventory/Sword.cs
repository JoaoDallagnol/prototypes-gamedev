using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private GameObject slashAnim;
    [SerializeField] private float swordAttackCD= 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;
    
    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAttackAnim;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    private void Start() {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }
    private void Update() {
        MouseFollowWithOffSet();
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void Attack() {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAttackAnim = Instantiate(slashAnim, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAttackAnim.transform.parent = transform.parent;
    }

    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent() {
        slashAttackAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
    
        if (PlayerController.Instance.FacingLeft) {
            slashAttackAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent() {
        slashAttackAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    
        if (PlayerController.Instance.FacingLeft) {
            slashAttackAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffSet() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        float angle = Mathf.Atan2(mousePos.y - playerPosition.y, Mathf.Abs(mousePos.x - playerPosition.x)) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
           ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
