using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawner : MonoBehaviour {
    [SerializeField] public Direction direction;
    private MobVariants _mobVariants;
    private GameObject _mob;
    private int _maxRoomWeight = 9;
    private static int _currentRoomWeight = 0;
    private int _rand;
    public enum Direction {
        LowWeightMobs,
        MediumWeightMobs,
        HighWeightMobs
    }
    private void Awake() {
        _mobVariants = GameObject.FindGameObjectWithTag("MobSpawner").GetComponent<MobVariants>();
    }
    private void Start() {
        Spawn();
    }public static void ResetCurrentWeight() {
        _currentRoomWeight = 0;
    }
    private void Spawn() {
        if (_currentRoomWeight < _maxRoomWeight) {
            SetRandomWeight();
           
            switch (direction) {
                case Direction.LowWeightMobs:
                _currentRoomWeight += 1;
                _rand = Random.Range(0, _mobVariants.lowWeightMobs.Length);
                _mob = Instantiate(_mobVariants.lowWeightMobs[_rand], transform.position, Quaternion.identity);
                Debug.Log("Создан моб в весом 1");
                break;
                case Direction.MediumWeightMobs:
                if (_currentRoomWeight + 2 > _maxRoomWeight) {
                    DecreaseDirection();
                    return;
                }
                _rand = Random.Range(0, _mobVariants.mediumWeightMobs.Length);
                _mob = Instantiate(_mobVariants.mediumWeightMobs[_rand], transform.position, Quaternion.identity);
                Debug.Log("Создан моб в весом 2");
                break;
                case Direction.HighWeightMobs:
                if (_currentRoomWeight + 3 > _maxRoomWeight) {
                    DecreaseDirection();
                    return;
                }
                _rand = Random.Range(0, _mobVariants.highWeightMobs.Length);
                _mob = Instantiate(_mobVariants.highWeightMobs[_rand], transform.position, Quaternion.identity);
                Debug.Log("Создан моб в весом 3");
                break;
            }
            if (_currentRoomWeight == _maxRoomWeight)
                ResetCurrentWeight();
            Destroy(gameObject);
        }
        Debug.Log(_currentRoomWeight);
    }
    
    private void DecreaseDirection() {
        int currentIndex = (int)direction;
        currentIndex--; 
        direction = (Direction)currentIndex; 
        Spawn();
    }
    private void SetRandomWeight() {
        Array directions = Enum.GetValues(typeof(Direction));
        int randomIndex = Random.Range(0, directions.Length); 
        direction = (Direction)directions.GetValue(randomIndex);
        }
    }
