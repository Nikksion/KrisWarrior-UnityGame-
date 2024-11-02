using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Utils;
using Unity.VisualScripting;
using System;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private State _startingState;
    //Roaming
    [SerializeField] private float _roamingDistnseMax = 5f;
    [SerializeField] private float _roamingDistnseMin = 1f;
    [SerializeField] private float _roamingTimerMax = 5f;
    private float _roamingSpeed; 
    private float _roamingTime;
    private Vector3 _roamPosition;
    //Agro
    [SerializeField] private float _agroDistance = 4f;
    [SerializeField] private float _chasingSpeed = 1f;
    [SerializeField] private State _state;
    //EnemySeting
    private Vector3 _startingPosition;
    //Attack
    [SerializeField] private float _attackSpeed = 5f;
    private float _attackDistance = 1.5f;
    private float _timeAttack = 0;

    private NavMeshAgent _navMeshAgent;

    public event EventHandler OnEnemyAttack;
    private enum State {
        Roaming,
        Chasing,
        Attacking,
        Death 
    }
    private void Start() {
        _startingPosition = transform.position;
        _navMeshAgent.SetDestination(_roamPosition);
        _enemyEntity.EnemyDie += _enemyEntity_EnemyDie;
    }
    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _state = State.Roaming;
        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed += _roamingSpeed;
    }
    private void Update() {
        StateHandler();
    }
    private void StateHandler() { 
        switch (_state) {
            default:
            case State.Attacking:
            AttackingTarget();
            GetCurrentState();
            break;
            case State.Death:
            break;
            case State.Chasing:
            ChasingTarget();
            GetCurrentState();
            break; 
            case State.Roaming:
            _roamingTime -= Time.deltaTime;
            if (_roamingTime < 0) {
                Roaming();
                _roamingTime = _roamingTimerMax;  
            }
            GetCurrentState();
            break;
        }
    }
    private void GetCurrentState() {
        float distanceToPlayer = Vector3.Distance(transform.position, GameInput.Instance.transform.position);
        State newState = _state;

        if ( distanceToPlayer > _agroDistance) 
            newState = State.Roaming;

        if (distanceToPlayer <= _agroDistance)
                newState = State.Chasing;

        if (distanceToPlayer <= _attackDistance) 
                newState = State.Attacking;
        
        if (newState != _state) {
            if (newState == State.Chasing) {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _chasingSpeed;
            }
            else if (newState == State.Attacking)
                _navMeshAgent.ResetPath();
            else if (newState == State.Death) {
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = 0;
                }
            else if (newState == State.Roaming) {
                _startingPosition = transform.position;
                _roamingTime = 4f;
            }
        }
         _state = newState;
    }
    private void AttackingTarget() {
        if (Time.time >= _timeAttack) {
             OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _timeAttack = Time.time + _attackSpeed;
        }
    }
    private void _enemyEntity_EnemyDie(object sender, EventArgs e) {
        _state = State.Death;
    }
    private void ChasingTarget() {
        _navMeshAgent.SetDestination(GameInput.Instance.transform.position);
        FlipNps(transform.position, GameInput.Instance.transform.position);

    }
    public bool IsChasing() {
        if (_navMeshAgent.velocity == Vector3.zero)
            return false;
        else return true;
    }
    private void Roaming() {
        _roamPosition = GetRoamingPosition();
        FlipNps(transform.position, _roamPosition);
        _navMeshAgent.SetDestination(_roamPosition);
    }
    private Vector3 GetRoamingPosition() {
        return _startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(_roamingDistnseMin, _roamingDistnseMax);
    }
    private void FlipNps(Vector3 sourgePosition, Vector3 targetPosition) {
        if (sourgePosition.x > targetPosition.x)
            transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }


}
