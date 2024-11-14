using System.Collections;
using UnityEngine;

public class GetSomeDoors : MonoBehaviour {
    [SerializeField] private GameObject wall;
    [SerializeField] private Transform spawnContainer;
    private Rigidbody rigidbody;
    private PolygonCollider2D polygonCollider;
    private bool _isDoorPresent = false;

    private void Awake() {
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void Start() {
        StartCoroutine(RemoveDoorsWithDelay());
    }
    public void OpenDoors() {
        polygonCollider.enabled = false;
    }
    public void CloseDoors() {
        polygonCollider.enabled =true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.CompareTag("Door"))
        _isDoorPresent = true; 
    }

    private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Door")) {
                _isDoorPresent = false;
            }
        } 

    

    private IEnumerator RemoveDoorsWithDelay() {
        // Ожидание 1 секунду
        yield return new WaitForSeconds(1f);

        // Проверка состояния isDoorPresent
        if (!_isDoorPresent) {
            Instantiate(wall, transform.position, transform.rotation, spawnContainer);
            Destroy(gameObject);
        }
    }
}