using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
    public class RogueVisual : MonoBehaviour {
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemyEntity _enemyEntity;
    private Animator _animator;

    private const string IS_ROUMING = "IsRouming";
    private const string IS_CHASING = "IsChasing";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string IS_DIE = "IsDie";
    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    private void Start() {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        _enemyEntity.EnemyDie += _enemyEntity_EnemyDie;
    }
    private void Update() {
        _animator.SetBool(IS_CHASING, _enemyAI.IsChasing());
    }
    private void OnDestroy() {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        _enemyEntity.EnemyDie -= _enemyEntity_EnemyDie;
    }
    public void TriggerAttackAnimationTurnOn() {
        _enemyEntity.PolygonColliderTurnOn();
    }
    public void TriggerAttackAnimationTurnOff() {
        _enemyEntity.PolygonColliderTurnOff();
    }
    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e) {
        int randomAttack = Random.Range(0, 2);
        if (randomAttack == 0) 
            _animator.SetTrigger(ATTACK1);
        else _animator.SetTrigger(ATTACK2);
    }
    private void _enemyEntity_EnemyDie(object sender, System.EventArgs e) {
        _animator.SetTrigger(IS_DIE);    
    }
}
