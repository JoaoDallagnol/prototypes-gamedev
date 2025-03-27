using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    // GERALMENTE Update é bom para Player Input
    private void Update() {
        PlayerInput();
    }
    
    // GERALMENTE FixedUpdate é bom para Fisicas
    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move() {
        // Como estamos utilizando o fixedUpdate, então o fixedDeltaTime é mais performatico
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
