using UnityEngine;

public class PickUpSpawner : MonoBehaviour {
    [SerializeField] private GameObject goldenCoinPrefab;

    public void DropItems() {
        Instantiate(goldenCoinPrefab, transform.position, Quaternion.identity);
    }
}
