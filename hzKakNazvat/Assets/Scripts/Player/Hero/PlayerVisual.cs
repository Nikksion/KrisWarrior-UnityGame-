using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    private Animator _animator;
    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private const string IS_RUN_SIDE = "IsRunSide";
    private const string IS_RUN_BACK = "IsRunBack";
    private const string IS_RUN_FRONT = "IsRunFront";
    private const string IS_DIE = "IsDie";
    private const string IS_MOVE = "IsMove";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string ATTACK3 = "Attack3";
    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>(); 
        GetComponentInChildren<Weapon>();
    }
    private void OnEnable() {
        Weapon.OnWeaponAttack += AttackAnimation;
        Player.OnPlayerDied += DieAnimation;
        Player.OnPlayerDamageTaked += PlayerTakeDamage;
    }
    private void OnDisable() {
        Weapon.OnWeaponAttack -= AttackAnimation;
        Player.OnPlayerDied -= DieAnimation;
        Player.OnPlayerDamageTaked -= PlayerTakeDamage;
    }
    private void Update() {
        _animator.SetBool(IS_RUN_SIDE, GameInput.Instance.IsRunningSide());
        _animator.SetBool(IS_RUN_BACK, GameInput.Instance.IsRunningBack());
        _animator.SetBool(IS_RUN_FRONT, GameInput.Instance.IsRunningFront());
        _animator.SetBool(IS_MOVE, GameInput.Instance.IsMove());
    }
    private void FixedUpdate() {
        _spriteRenderer.flipX = GameInput.Instance.Flip();
    }
    public void EnableCollider() {
        _weapon.AttackColliderTurnOn();
    }
    public void DisableCollider() {
        _weapon.AttackColliderTurnOff();       
    }
    public void ImputTurnOn() {
        _player.InputFlagTurnOn();
    }
    public void ImputTurnOff() {
        _player.InputFlagTurnOff();
    }
    public void DestroyDeathPlayer() {
        _player.DestroyPlayer();
    }
    private void AttackAnimation(int number) {
        switch (number) {
            case 1:
            _animator.SetTrigger(ATTACK1);
            break;
            case 2:
            _animator.SetTrigger(ATTACK2);
            break;
            case 3:
            _animator.SetTrigger(ATTACK3);
            break;
        }
    }
    private void PlayerTakeDamage() {
        _spriteRenderer.color = UnityEngine.Color.red;
        StartCoroutine(ResetColorAfterDelay());
    }
    private IEnumerator ResetColorAfterDelay() {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = UnityEngine.Color.white;
    }
    private void DieAnimation() {
        _animator.SetBool(IS_DIE, true);
    }
}

