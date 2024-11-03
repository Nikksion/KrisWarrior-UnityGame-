using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class WarriorPlayer : AbstractPlayer {
    private void Start() {
        _rb.position = Vector2.zero; // start pos
        _health = _maxHealth;
        _armor = _maxArmor;
        OnPlayerHealthChanged?.Invoke(_maxArmor, _armor, _maxHealth, _health);
    }
    private void FixedUpdate() {
        MovementPlayer();
    }
    //Dash (доделать сохранение воследнего вектора)
    protected override IEnumerator DashCorrutain() {
        Debug.Log("dash");
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + GameInput.Instance.GetMovmentPlayer() * _dashSpeed;
        float duration = 0.2f;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition; // Устанавливаем конечную позицию
    }
}
