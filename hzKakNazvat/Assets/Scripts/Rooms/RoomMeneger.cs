using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMeneger : MonoBehaviour{
    private Rigidbody _rigidbody;
    private BoxCollider2D _boxCollider;
    private MobSPSpawner _mobSPSpawner;
    private GetSomeDoors[] _doors;
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _mobSPSpawner = GetComponentInChildren<MobSPSpawner>(true);
        _doors = GetComponentsInChildren <GetSomeDoors>();
    }
    private void OnTriggerStay2D(Collider2D collision) {
        bool playerInTrigger = collision.CompareTag("Player");
        bool mobInTrigger = false;
        Collider2D[] collidersInTrigger = Physics2D.OverlapCircleAll(transform.position, GetComponent<Collider2D>().bounds.extents.x);
        foreach (var collider in collidersInTrigger) {
            if (collider.CompareTag("Mob")) {
                mobInTrigger = true;
                break; 
            }
        }
        if (playerInTrigger) {
            if (_doors != null) {
                if (!mobInTrigger) {
                    foreach (var door in _doors) 
                        if (door != null) {
                            door.OpenDoors();
                    }
                }
                else 
                    foreach (var door in _doors) 
                        if (door != null)
                            door.CloseDoors();
            }
            else {
                Debug.LogWarning("Массив _doors равен null.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && _mobSPSpawner != null)
            _mobSPSpawner.EnableMobSPSpawner();
    }
    private void GetDoors() {
        
    }
}
