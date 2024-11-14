using System.Collections;
using UnityEngine;

public class CameraFollo : MonoBehaviour {
    [SerializeField] private GameObject _object;
    [SerializeField] private Vector3 _distanceFromObject;

    // Параметры сотрясения
    [SerializeField] private float shakeDuration = 0.1f; 
    [SerializeField] private float shakeMagnitude = 2f; 

    private void OnEnable() {
        AbstractPlayer.OnPlayerDamageTaked += ShakeCamera; 
    }

    private void OnDisable() {
        AbstractPlayer.OnPlayerDamageTaked -= ShakeCamera; 
    }

    private void LateUpdate() {
    }

    private void ShakeCamera() {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    private IEnumerator Shake(float duration, float magnitude) {
        Vector3 originalPos = transform.localPosition; // Сохраняем оригинальную позицию
        float elapsed = 0f;

        while (elapsed < duration) {
            float x = Random.Range(-2f, 4f) * magnitude; 
            float y = Random.Range(-2f, 4f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos; // Восстанавливаем оригинальную позицию
    }
}