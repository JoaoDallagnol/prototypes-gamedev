using UnityEngine;

public class Destructible : MonoBehaviour {
    [SerializeField] private GameObject destryVFX;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<DamageSource>()) {
            Instantiate(destryVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
