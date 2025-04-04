using UnityEngine;

public class CameraMotor : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform lookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private void Start() {
        lookAt = GameObject.Find("Player").transform;
    }
    private void LateUpdate() {
        Vector3 delta = Vector3.zero;

        // this is to check if we're inside the bound on the X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX) {
            if (transform.position.x < lookAt.position.x) {
                delta.x = deltaX - boundX;
            } else {
                delta.x = deltaX + boundX;
            }
        }

        // this is to check if we're inside the bound on the Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY) {
            if (transform.position.y < lookAt.position.y) {
                delta.y = deltaY - boundY;
            } else {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
