using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyEntity : MonoBehaviour {
    [SerializeField] private int _maxHealth;
    private int _health;
    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    public event EventHandler EnemyDie;
    private void Awake() {
        _polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Start() {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage) {
        _health -= damage;
        Death();
    }
    public void PolygonColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }
    public void PolygonColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    } public void BoxColliderTurnOff() {
        _boxCollider2D.enabled = false;
    }
    private void Death() {
        if (_health <= 0) {
            BoxColliderTurnOff();
            PolygonColliderTurnOff();
            EnemyDie?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
