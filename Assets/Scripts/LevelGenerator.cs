using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _spawnedObjects;
    [SerializeField] private float _delayOfGeneration = 2f;
    [SerializeField] private float _xSpawnPositionOffsetFromPlayer;
    [SerializeField] private float _zSpawnPosition = 0f;
    
    private WaitForSeconds _destroyDelay = new WaitForSeconds(10f);
    private IEnumerator<WaitForSeconds> _levelGeneratorCoroutine;

    private Vector3 GetSpawnPosition() {
        Vector3 spawnPosition = _player.transform.position;
        spawnPosition.x += _xSpawnPositionOffsetFromPlayer;
        spawnPosition.z = _zSpawnPosition;
        return spawnPosition;
    }

    IEnumerator<WaitForSeconds> GenerateLevel() {
        Vector3 spawnPosition;
        while (true) {
            yield return new WaitForSeconds(_delayOfGeneration);

            Debug.Log("Spawn");

            float playerHealth = _player.GetComponent<HealthComponent>().Health;
            //float healthDifference = 1f;
            //_spawnedObject.GetComponent<HealthComponent>().Health = playerHealth - healthDifference;
            foreach (var _spawnedObject in _spawnedObjects) {
                GameObject newBlock = Instantiate(_spawnedObject);
                newBlock.transform.position = GetSpawnPosition();

                StartCoroutine(DestroyAfterTime(newBlock));
            }
            
        }
    }

    IEnumerator<WaitForSeconds> DestroyAfterTime(GameObject destroyedObject) {
        yield return _destroyDelay;
        Destroy(destroyedObject);
    }

    private void Start() {
        _levelGeneratorCoroutine = GenerateLevel();
        StartCoroutine(_levelGeneratorCoroutine);
    }

    private void OnDestroy() {
        StopCoroutine(_levelGeneratorCoroutine);
    }
}
