using UnityEngine;

public class Sword : MonoBehaviour {
    private PlayerControls playerControls;
    private Animator myAnimator;

    private void Awake() {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack() {
        // fire our sword animation
        
        myAnimator.SetTrigger("Attack");
    }
}
