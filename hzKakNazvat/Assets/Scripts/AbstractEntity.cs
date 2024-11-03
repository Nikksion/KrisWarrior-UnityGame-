using System.Collections;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour {
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    public virtual void TakeDamage(int damage) {
        _health -= damage;
        Death();
    }
    protected abstract void Death();
}