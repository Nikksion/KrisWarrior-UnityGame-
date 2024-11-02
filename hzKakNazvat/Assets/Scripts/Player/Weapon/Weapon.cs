using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _weaponDamage = 1500;
    private PolygonCollider2D _polygonCollider2D;
    private int _attackNumber = 1;

    public static Action<int> OnWeaponAttack;//собыитие атаки
    public void Awake() {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    public void Start() {
        AttackColliderTurnOff();
    }
    //атака со счетчиком(доделать сбрасывание по времени)!!!
    public void Attack() {
        ChangeColliderPoint(_attackNumber);
        OnWeaponAttack?.Invoke(_attackNumber);
        _attackNumber++;
        _attackNumber = _attackNumber >= 4 ? 1: _attackNumber;
    }
    public void WeaponFlipQuaternion(float x, float y, float z) {
        transform.rotation = Quaternion.Euler(x, y, z);
    }
    public void AttackColliderTurnOn() {
        _polygonCollider2D.enabled = true;
    }
    public void AttackColliderTurnOff() {
        _polygonCollider2D.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)){
            Debug.Log("EnemyTakeDamage");
            enemyEntity.TakeDamage(_weaponDamage);
        }
    }
    //Установка точек для разного типа аттаки
    private void ChangeColliderPoint(int attackNumber) {
        Vector2[] points = _polygonCollider2D.points;
        switch (attackNumber) {
            case 1:
            points[0] = new Vector2(0f, 1.4f);    
            points[1] = new Vector2(-1.3f, 0.9f); 
            points[2] = new Vector2(-1.7f, -0.2f);  
            points[3] = new Vector2(-1.3f, -1.1f);  
            points[4] = new Vector2(0f, -1.6f);     
            break;
            case 2:
            points[0] = new Vector2(0.5f, 0.55f);   
            points[1] = new Vector2(-1.5f, 0.8f);   
            points[2] = new Vector2(-1.8f, 0.05f);     
            points[3] = new Vector2(-1.5f, -0.8f);  
            points[4] = new Vector2(0.5f, -0.65f);
            break;
            case 3:
            points[0] = new Vector2(1.25f, 1f);   
            points[1] = new Vector2(-1.4f, 1f);   
            points[2] = new Vector2(-1.4f, -1.25f);   
            points[3] = new Vector2(1.2f, -1.25f);    
            points[4] = new Vector2(0.1f, 0f);    
            break;
        }
        _polygonCollider2D.points = points;
    }

    

}
