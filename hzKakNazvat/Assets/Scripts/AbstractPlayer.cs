using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class AbstractPlayer : AbstractEntity
{
    [SerializeField] protected int _maxArmor = 3;
    [SerializeField] protected int _armor;
    [SerializeField] protected float _dashSpeed = 1.1f;
    [SerializeField] protected Weapon _weapon;

    protected Rigidbody2D _rb;
    protected BoxCollider2D _boxCollider;
    protected CapsuleCollider2D _capsuleCollider;

    protected bool _isArmorRegenActive = false;
    protected bool _canInput = true;
    protected IEnumerator _armorRegenCoroutine; // Корутину для восстановления брони

    public static System.Action<int, int, int, int> OnPlayerHealthChanged; // Событие изменения здоровья и брони
    public static System.Action OnPlayerDamageTaked; // Получение урона
    public static System.Action OnPlayerDied; // Смерть
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        GetComponentInChildren<Weapon>();
        GetComponentInChildren<PlayerVisual>();
    }
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
    public void DestroyPlayer() {
        Destroy(gameObject);
    }
    public override void TakeDamage(int damage) {
        if (_isArmorRegenActive) {
            StopCoroutine(_armorRegenCoroutine);
            _isArmorRegenActive = false;
        }
        if (_armor <= 0) {
            _health--;
            Death();
        }
        else
            _armor--;
        if (_armor < _maxArmor) {
            _armorRegenCoroutine = ArmorRegenetarionCoroutine();
                _isArmorRegenActive = true;
               
            StartCoroutine(_armorRegenCoroutine);
        }
        OnPlayerHealthChanged?.Invoke(_maxArmor, _armor, _maxHealth, _health);
        OnPlayerDamageTaked?.Invoke();
    }
    public void OnClickDashButtom() {
        if (_canInput)
            StartCoroutine(DashCorrutain());
    } 
    protected override void Death() {
        if (_health <= 0) {
            CapsuleColliderTurnOff();
            OnPlayerDied?.Invoke();
        }
    }
    protected void MovementPlayer() {
        if (_canInput)
            _rb.velocity = GameInput.Instance.GetMovmentPlayer() * _moveSpeed;
        else
            _rb.velocity = new Vector2(0f, 0f);
    }
    private IEnumerator ArmorRegenetarionCoroutine() {
        _isArmorRegenActive = true;
        _isArmorRegenActive = true;
        yield return new WaitForEndOfFrame();
        while (_armor < _maxArmor) {
            yield return new WaitForSeconds(5f);
            Debug.Log("Regenerating Armor");
            _armor++;
            OnPlayerHealthChanged?.Invoke(_maxArmor, _armor, _maxHealth, _health);
        }
        _isArmorRegenActive = false;
    }
 
    protected abstract IEnumerator DashCorrutain();
}
