using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //enum TypeOfSpawnedObjects {
    //    NONE,
    //    UNRANDOM_BLOCKS,
    //    RANDOM_BLOCKS,
    //    RANDOM_HEALER
    //}

    //struct SetOfSpawnedObjects {
    //    public GameObject SetOfGameObjects;
    //    public TypeOfSpawnedObjects TypeOfSpawnedObjects;
    //}

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _spawnedObjects;
    [SerializeField] private float _delayOfGeneration = 2f;
    [SerializeField] private float _xSpawnPositionOffsetFromPlayer;
    [SerializeField] private float _zSpawnPosition = 0f;
    [SerializeField] private float _xPlayerTriggerDifference = 0f;
    //[SerializeField] private SetOfSpawnedObjects[] _setOfObjects;

    private Vector3 _lastPlayerPostion;
    private WaitForSeconds _destroyDelay = new WaitForSeconds(10f);
    private IEnumerator<WaitForSeconds> _levelGeneratorCoroutine;

    private void Start() {
        _lastPlayerPostion = _player.transform.position;
        _levelGeneratorCoroutine = GenerateLevel();
        StartCoroutine(_levelGeneratorCoroutine);
    }

    private void OnDestroy() {
        StopCoroutine(_levelGeneratorCoroutine);
    }

    IEnumerator<WaitForSeconds> GenerateLevel() {
        for (int indexOfObject = 0; ; indexOfObject = (indexOfObject + 1) % _spawnedObjects.Length) {
            yield return new WaitForSeconds(_delayOfGeneration);
            SpawnObject(_spawnedObjects[indexOfObject]);
        }
    }

    private GameObject SpawnObject(GameObject spawnedObject) {
        GameObject newSetOfBlocks = SpawnObjectClone(spawnedObject);
        SetPosition(ref newSetOfBlocks);
        RandomDeleteChildren(ref newSetOfBlocks);
        SetHealthToChildren(ref newSetOfBlocks);
        StartCoroutine(DestroyAfterTime(newSetOfBlocks));
        return newSetOfBlocks;
    }

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

    private GameObject SpawnObjectClone(GameObject spawnedObject) {
        GameObject objectClone = Instantiate(spawnedObject);
        return objectClone;
    }

    private void SetPosition(ref GameObject gameObject) {
        gameObject.transform.position = GetSpawnPosition();
    }

    private void RandomDeleteChildren(ref GameObject newSetOfBlocks) {
        var rand = new System.Random();
        var chanceToDestroy = 50; // from 0 to 100

        int minNumber = 0;
        int maxNumber = 101;
        if (chanceToDestroy < minNumber || maxNumber < chanceToDestroy) {
            throw new Exception("Bad chanceToDestroy in LevelGenerator");
        }

        for (int indexOfChild = 0; indexOfChild < newSetOfBlocks.transform.childCount; indexOfChild++) {
            if (rand.Next(minNumber, maxNumber) < chanceToDestroy) {
                Destroy(newSetOfBlocks.transform.GetChild(indexOfChild).gameObject);
            }
        }
    }

    // Set one of block hp less than on the player becouse it's the ability to play
    private void SetRandomBlockHealth(ref HealthComponent[] healthOfBlocks, System.Random rand, int newHealth) {
        if (healthOfBlocks.Length != 0) {
            int indexOfPlayerHPBlock = rand.Next(0, healthOfBlocks.Length);
            healthOfBlocks[indexOfPlayerHPBlock].Health = newHealth;
        }
    }

    private void SetHealthToChildren(ref GameObject newSetOfBlocks) {
        System.Random rand = new System.Random();
        int playerHealth = (int)_player.GetComponent<HealthComponent>().Health;
        int percentageMultiplierPlayerHP = 80; // From 0 to 100
        int healthDifference = 1;

        HealthComponent[] healthOfBlocks = newSetOfBlocks.GetComponentsInChildren<HealthComponent>();
        foreach (var block in healthOfBlocks) {
            block.Health = playerHealth * (1 + rand.Next(0, percentageMultiplierPlayerHP) / 100f); 
        }

        SetRandomBlockHealth(ref healthOfBlocks, rand, playerHealth - healthDifference);
    }

    
}
