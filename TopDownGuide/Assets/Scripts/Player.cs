using UnityEngine;

public class Player : Mover {
    private SpriteRenderer spriteRenderer;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);

        xSpeed = 1.5f;
        ySpeed = 1.5f;
    }
    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId) {
        spriteRenderer.sprite = GameManager.instance.playerSprite[skinId];
    }

    public void OnLevelUp() {
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level) {
        for (int i = 0; i < level; i++) {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount) {
        if (hitPoint == maxHitPoint) {
            return;
        }

        hitPoint += healingAmount;
        if (hitPoint > maxHitPoint) {
            hitPoint = maxHitPoint;
        }

        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }
}
