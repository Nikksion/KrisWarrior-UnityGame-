using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSPSpawner : MonoBehaviour{
    [SerializeField]private GameObject _spawnPoint;
    private int _maxSpawnPoints = 7;
    private int _minSpawnPoints = 4;
    private int _maxSP;
    private int _sPCount = 0;

    private void OnEnable() { 
        _maxSP = Random.Range(_minSpawnPoints, _maxSpawnPoints++);
        Spawn();
    }
    public void EnableMobSPSpawner() {
        gameObject.SetActive(true);
    }
    private void Spawn() {
        if (_sPCount < _maxSP) {
            _spawnPoint = Instantiate(_spawnPoint, transform.position + RandomPosition(), Quaternion.identity);
            _sPCount++;
            Debug.Log("точка заспавнена");
            Spawn(); }
        else
            Destroy(gameObject, 1f);
   }
   private Vector3 RandomPosition() {
        float randomX = Random.Range(-10f, 10f); 
        float randomY = Random.Range(-4f, 4f);
        Vector3 vector3 = new Vector3(randomX,randomY,0f);
        return vector3;

    }

}
