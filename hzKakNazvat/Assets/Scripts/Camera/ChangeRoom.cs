using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
   [SerializeField] private Vector3 cameraChangedPos;
   [SerializeField] private Vector3 playerChangedPos;
    private Camera camera;
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.transform.position += playerChangedPos;
            camera.transform.position += cameraChangedPos;
        }
    }

}
