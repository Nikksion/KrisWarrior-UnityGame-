using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Player : MonoBehaviour {
    [SerializeField] private int _playerMaxHealth = 3;
    [SerializeField] private int _playerHealth;
    [SerializeField] private int _playerMaxArmor = 3;
    [SerializeField] private int _playerArmor;
    [SerializeField] private float _playerMoveSpeed = 5f;
    [SerializeField] private float _playerDashSpeed =1.1f;
    [SerializeField] private Weapon _weapon;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private CapsuleCollider2D _capsuleCollider;

    private bool _isArmorRegenActive = false;
    private bool _canInput = true;

    private IEnumerator _armorRegenCoroutine;//корутина для воостановления брони

    public static Action<int, int, int, int> OnPlayerHealthChanged;//событие изменения здоровья и брони
    public static Action OnPlayerDamageTaked;//полечения урона
    public static Action OnPlayerDied;//смерти
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        GetComponentInChildren<Weapon>();
        GetComponentInChildren<PlayerVisual>();
    }
    private void Start() {
        _rb.position = Vector2.zero; // start pos
        _playerHealth = _playerMaxHealth;
        _playerArmor = _playerMaxArmor;
        OnPlayerHealthChanged?.Invoke(_playerMaxArmor, _playerArmor, _playerMaxHealth, _playerHealth);
    }
    private void FixedUpdate() {
        MovementPlayer();
    }
    private void OnDestroy() {
        
    }
    //возможность нажимать конопки
    public bool GetInputFlag() {
    return _canInput; 
    }
    public void InputFlagTurnOn() {
        _canInput = true;
    }
    public void InputFlagTurnOff() {
        _canInput = false;
    }
    public void BoxColliderTurnOff() {
        _boxCollider.enabled = false;
    }
    public void CapsuleColliderTurnOff() {
        _capsuleCollider.enabled = false;
    }
    public void BoxColliderTurnOn() {
        _boxCollider.enabled = true;
    }
    public void CapsuleColliderTurnOn() {
        _capsuleCollider.enabled = true;
    }
    public void OnClickAttackButton() {
        if (_canInput)
            _weapon.Attack();
    }
    public void OnClickDashButtom() {
        if (_canInput)
           StartCoroutine(Dash());
    }  
    public void PlayerTakeDamage() {
        if (_isArmorRegenActive) {
            StopCoroutine(_armorRegenCoroutine);
            _isArmorRegenActive = false;
        }
        if (_playerArmor <= 0) {
            _playerHealth--;
            PlayerDeath();
        }
        else 
            _playerArmor--;
        if (_playerArmor < _playerMaxArmor) {
            _armorRegenCoroutine = ArmorRegenetarion();
            StartCoroutine(_armorRegenCoroutine);
        }
        OnPlayerHealthChanged?.Invoke(_playerMaxArmor, _playerArmor, _playerMaxHealth, _playerHealth);
        OnPlayerDamageTaked?.Invoke();
    }
    private void PlayerDash() {
        _rb.velocity = new Vector2(0, 0);
        _rb.AddForce(GameInput.Instance.GetMovmentPlayer() * 8000);
    }
    private IEnumerator ArmorRegenetarion() {
        _isArmorRegenActive = true;
        yield return new WaitForEndOfFrame();
        while (_playerArmor < _playerMaxArmor) {
            yield return new WaitForSeconds(5f);
            Debug.Log("Regenerating Armor");
            _playerArmor++;
            OnPlayerHealthChanged?.Invoke(_playerMaxArmor, _playerArmor, _playerMaxHealth, _playerHealth);
        }
        _isArmorRegenActive = false;
    }
    //Dash (доделать сохранение воследнего вектора)
    private IEnumerator Dash() {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + GameInput.Instance.GetMovmentPlayer() * _playerDashSpeed; 
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
    private void PlayerDeath() {
        if (_playerHealth <= 0) {
            CapsuleColliderTurnOff();
            OnPlayerDied?.Invoke();
        }
    }
    public void DestroyPlayer() {
        Destroy(gameObject);
    }
    private void MovementPlayer() {
        if (_canInput)
            _rb.velocity = GameInput.Instance.GetMovmentPlayer() * _playerMoveSpeed;
        else
            _rb.velocity = new Vector2(0f, 0f);
    }
}
