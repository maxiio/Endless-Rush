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
    [SerializeField] private float _xPlayerTriggerDifference = 0f;


    private Vector3 _lastPlayerPostion;
    private WaitForSeconds _destroyDelay = new WaitForSeconds(10f);
    private IEnumerator<WaitForSeconds> _levelGeneratorCoroutine;

    private Vector3 GetSpawnPosition() {
        Vector3 spawnPosition = _player.transform.position;
        spawnPosition.x += _xSpawnPositionOffsetFromPlayer;
        spawnPosition.z = _zSpawnPosition;
        return spawnPosition;
    }

    IEnumerator<WaitForSeconds> DestroyAfterTime(GameObject destroyedObject) {
        yield return _destroyDelay;
        Destroy(destroyedObject);
    }

    private GameObject SpawnObjectByIndex(int index) {
        GameObject newBlock = Instantiate(_spawnedObjects[index]);
        newBlock.transform.position = GetSpawnPosition();
        StartCoroutine(DestroyAfterTime(newBlock));
        return newBlock;
    }

    IEnumerator<WaitForSeconds> GenerateLevel() {
        for (int indexOfObject = 0; ; indexOfObject = (indexOfObject + 1) % _spawnedObjects.Length) {
            yield return new WaitForSeconds(_delayOfGeneration);
            SpawnObjectByIndex(indexOfObject);            
        }
    }

    private void Start() {
        _lastPlayerPostion = _player.transform.position;
        _levelGeneratorCoroutine = GenerateLevel();
        StartCoroutine(_levelGeneratorCoroutine);
    }

    private void OnDestroy() {
        StopCoroutine(_levelGeneratorCoroutine);
    }
}
