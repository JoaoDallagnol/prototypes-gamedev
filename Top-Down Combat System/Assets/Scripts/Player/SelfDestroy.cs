using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    private ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    public void DestroySelf() {
        Destroy(gameObject);
    }

    private void Update() {
        if (ps && !ps.IsAlive()) {
            DestroySelf();
        }
    }
}
