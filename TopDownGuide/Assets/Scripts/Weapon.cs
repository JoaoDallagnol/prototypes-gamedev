using System;
using UnityEngine;

public class Weapon : Collidable {

    //Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8};
    public float[] pushForce = {1.8f, 2.0f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f};

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space)) {
            lastSwing = Time.time;
            Swing();
        }
    }

    private void Swing() {
        anim.SetTrigger("Swing");
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            if (coll.name == "Player") {
                return; 
            }

            //Create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
