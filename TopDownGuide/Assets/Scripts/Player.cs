using UnityEngine;

public class Player : MonoBehaviour {
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Reset de moveDelta
        moveDelta = new Vector3(x,y,0);

        // Swap sprite direction, wether you're going right or left
        if (moveDelta.x > 0) 
            transform.localScale = Vector3.one;
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //Move
        transform.Translate(moveDelta * Time.deltaTime);
    }
}
