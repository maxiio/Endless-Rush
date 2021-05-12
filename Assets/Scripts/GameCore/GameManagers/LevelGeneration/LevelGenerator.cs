using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    enum TypeOfSpawnedObjects {
        NONE,
        UNRANDOM_BLOCKS,
        RANDOM_BLOCKS,
        RANDOM_HEALER
    }

    [Serializable]
    struct SetOfSpawnedObjects {
        public GameObject SetOfGameObjects;
        public TypeOfSpawnedObjects TypeOfSpawnedObjects;
    }

    [SerializeField] private GameObject _player;
    [SerializeField] private float _zSpawnPosition;
    [SerializeField] private float _xSpawnPositionOffsetFromPlayer;
    [SerializeField] private float _delayOfCheckTriggerSpawn = 0.5f;
    [SerializeField] private float _xPlayerTriggerDifference;
    [SerializeField] private float _xDistanceToClean = 10;
    [SerializeField] private SetOfSpawnedObjects[] _setOfObjects;

    // The minumum health difference with player whick random block should have
    private const int _blockHealthDifferenceWithPlayer = 1;

    // Chance from 0 to 100
    private readonly int chanceToDestroyRandomBlocks = 60;
    private readonly int chanceToDestroyHealers = 70;

    /// <summary>
    /// Multiplier where 1 - default, 3 - multiply by 3
    /// This range for random which multiply by playerHealth
    /// </summary>
    private readonly (float, float) multiplierStaticBlocksHP = (0.4f, 2f);
    private readonly (float, float) multiplierRandomBlocksHP = (0.2f, 2f);
    private readonly (float, float) multiplierHealerHP = (0.2f, 1.6f);

    private WaitForSeconds _checkPlayerInDestroyTrigger = new WaitForSeconds(1f);
    System.Random _rand = new System.Random();
    private IEnumerator<WaitForSeconds> _levelGeneratorCoroutine;

    private void Start() {
        _levelGeneratorCoroutine = GenerateLevel();
        StartCoroutine(_levelGeneratorCoroutine);
    }

    private void OnDestroy() {
        StopCoroutine(_levelGeneratorCoroutine);
    }

    IEnumerator<WaitForSeconds> GenerateLevel() {
        float playerLastPositionX = _player.transform.position.x;

        for (int i = 0; ; i = (i + 1) % _setOfObjects.Length) {
            while (playerLastPositionX + _xPlayerTriggerDifference > _player.transform.position.x) {
                yield return new WaitForSeconds(_delayOfCheckTriggerSpawn);
            }
            playerLastPositionX = _player.transform.position.x;
            SpawnObject(_setOfObjects[i].SetOfGameObjects, _setOfObjects[i].TypeOfSpawnedObjects);
            yield return new WaitForSeconds(_delayOfCheckTriggerSpawn);
        }
    }

    private GameObject SpawnObject(GameObject spawnedObject, TypeOfSpawnedObjects typeOfSpawned) {
        GameObject newSetOfBlocks = SpawnObjectClone(spawnedObject);
        SetPosition(ref newSetOfBlocks);
        switch (typeOfSpawned) {            
            case TypeOfSpawnedObjects.UNRANDOM_BLOCKS:
                SetHealthToChildren(ref newSetOfBlocks, multiplierStaticBlocksHP);
                break;
            case TypeOfSpawnedObjects.RANDOM_BLOCKS:
                RandomDeleteChildren(ref newSetOfBlocks, chanceToDestroyRandomBlocks);
                SetHealthToChildren(ref newSetOfBlocks, multiplierRandomBlocksHP);
                break;
            case TypeOfSpawnedObjects.RANDOM_HEALER:
                RandomDeleteChildren(ref newSetOfBlocks, chanceToDestroyHealers);
                SetHealthToChildren(ref newSetOfBlocks, multiplierHealerHP);
                break;
            case TypeOfSpawnedObjects.NONE:
                throw new ArgumentException("TypeOfSpawnedObjects.NONE is getting");
            default:
                throw new ArgumentException("Default is getting");
        }
        StartCoroutine(DestroyAfterPlayer(newSetOfBlocks));

        return newSetOfBlocks;
    }

    private Vector3 GetSpawnPosition() {
        Vector3 spawnPosition = _player.transform.position;
        spawnPosition.x += _xSpawnPositionOffsetFromPlayer;
        spawnPosition.z = _zSpawnPosition;
        return spawnPosition;
    }

    private IEnumerator<WaitForSeconds> DestroyAfterPlayer(GameObject destroyedObject) {
        while (destroyedObject.transform.position.x + _xDistanceToClean > _player.transform.position.x) {
            yield return _checkPlayerInDestroyTrigger;
        }
        Destroy(destroyedObject);
    }

    private GameObject SpawnObjectClone(GameObject spawnedObject) {
        GameObject objectClone = Instantiate(spawnedObject);
        return objectClone;
    }

    private void SetPosition(ref GameObject gameObject) {
        gameObject.transform.position = GetSpawnPosition();
    }

    private void RandomDeleteChildren(ref GameObject newSetOfBlocks, int chanceToDestroy) {
        var rand = new System.Random();
        if (chanceToDestroy < 0 || 100 < chanceToDestroy ) {
            throw new ArgumentOutOfRangeException("chanceToDestroy");
        }

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

    private void SetHealthToChildren(ref GameObject newSetOfBlocks, (float, float) multiplierHP) {

        int healthDifference = _blockHealthDifferenceWithPlayer;
        int playerHealth = (int)_player.GetComponent<HealthComponent>().Health;
        HealthComponent[] healthOfBlocks = SetRandomHealthToBlocks(newSetOfBlocks, playerHealth, multiplierHP);
        healthOfBlocks = SetRandomBlockHealth(healthOfBlocks, playerHealth - healthDifference);
    }

    private HealthComponent[] SetRandomHealthToBlocks(GameObject newSetOfBlocks, int baseHealth, (float, float) multiplierHP) {
        HealthComponent[] healthOfBlocks = newSetOfBlocks.GetComponentsInChildren<HealthComponent>();
        foreach (var block in healthOfBlocks) {
            block.Health = Mathf.Round(baseHealth * _rand.Next((int)(multiplierHP.Item1 * 100), (int)(multiplierHP.Item2 * 100)) / 100f);
        }
        return healthOfBlocks;
    }

    // Set one of block hp less than on the player because it's the ability to play
    private HealthComponent[] SetRandomBlockHealth(HealthComponent[] objectsHealth, int newHealth) {
        if (objectsHealth.Length != 0) {
            int indexOfPlayerHPBlock = _rand.Next(0, objectsHealth.Length);
            objectsHealth[indexOfPlayerHPBlock].Health = newHealth;
        }
        return objectsHealth;
    }
}
