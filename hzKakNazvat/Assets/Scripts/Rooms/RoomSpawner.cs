using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    [SerializeField] public Direction direction;
    [SerializeField] private RoomVariants _variants;
    private GameObject _spawnedRoom;
    private NavMeshSurfeces _navMeshSurfeces;
    private int _rand;
    private bool _spawned = false;
    private static int _roomCount = 0;
    private const int _maxRooms = 8;
    private const float _resetTime = 0.9f;
    private static float _timeSinceLastSpawn = 0f;

    private static List<GameObject> _spawnedRooms = new List<GameObject>();

    public enum Direction {
        Top,
        Left,
        Right,
        Bottom,
        None
    }
    private void Awake() {
        _navMeshSurfeces = GameObject.FindGameObjectWithTag("NavMeshSurfece").GetComponent<NavMeshSurfeces>();
    }
    private void Start() {
        _variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Invoke("Spawn", 0.05f);
    }

    private void Update() {
        if (_spawned) {
            _timeSinceLastSpawn += Time.deltaTime;
        }

        if (_timeSinceLastSpawn >= _resetTime && _roomCount != _maxRooms) {
            RemoveAllRooms();
            Invoke("Spawn", 0.05f);
        }
        if (_roomCount == _maxRooms)
            Destroy(gameObject);
    }

    private void Spawn() {
        if (!_spawned && _roomCount < _maxRooms) {
            Vector2 spawnPosition = transform.position;
            if (CanSpawn()) {
                if (_roomCount == _maxRooms - 1) {
                    _rand = Random.Range(0, _variants.BossRooms.Length);
                    _spawnedRoom = Instantiate(_variants.BossRooms[_rand], spawnPosition, Quaternion.identity);
                }
                else if (_roomCount == _maxRooms - 2) {
                    _rand = Random.Range(0, _variants.InterestRooms.Length);
                    _spawnedRoom = Instantiate(_variants.InterestRooms[_rand], spawnPosition, Quaternion.identity);
                }
                else {
                    switch (direction) {
                        case Direction.Top:
                        _rand = Random.Range(0, _variants.topRooms.Length);
                        _spawnedRoom = Instantiate(_variants.topRooms[_rand], spawnPosition, Quaternion.identity);
                        break;
                        case Direction.Bottom:
                        _rand = Random.Range(0, _variants.bottomRooms.Length);
                        _spawnedRoom = Instantiate(_variants.bottomRooms[_rand], spawnPosition, Quaternion.identity);
                        break;
                        case Direction.Left:
                        _rand = Random.Range(0, _variants.leftRooms.Length);
                        _spawnedRoom = Instantiate(_variants.leftRooms[_rand], spawnPosition, Quaternion.identity);
                        break;
                        case Direction.Right:
                        _rand = Random.Range(0, _variants.rightRooms.Length);
                        _spawnedRoom = Instantiate(_variants.rightRooms[_rand], spawnPosition, Quaternion.identity);
                        break;
                    }
                }
                _spawned = true;
                _roomCount++;
                _spawnedRooms.Add(_spawnedRoom);
                _timeSinceLastSpawn = 0f;
            }
            else {
                Debug.Log("Ќевозможно заспавнить комнату: место зан€то или превышено максимальное количество комнат");
            }
        }
        else if (_roomCount >= _maxRooms) {
            _navMeshSurfeces.NavMeshSurfaceBake();
            Debug.Log("ƒостигнуто максимальное количество комнат");
        }
    }

    private bool CanSpawn() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        return colliders.Length == 0;
    }

    private void RemoveAllRooms() {
        foreach (GameObject room in _spawnedRooms) {
            Destroy(room);
        }
        _spawnedRooms.Clear();
        _roomCount = 0;
        Debug.Log("¬се комнаты удалены и процесс начат заново.");
        _spawned = false;
    }
}