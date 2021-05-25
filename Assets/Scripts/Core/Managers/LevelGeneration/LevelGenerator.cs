using System;
using System.Collections.Generic;
using Core.Components.Health;
using UnityEngine;
// ReSharper disable InconsistentNaming
// ReSharper disable IteratorNeverReturns

namespace Core.Managers.LevelGeneration {
	public class LevelGenerator : MonoBehaviour {
		private enum TypeOfSpawnedObjects {
			NONE,
			NOT_RANDOM_BLOCKS,
			RANDOM_BLOCKS,
			RANDOM_HEALER
		}

		[Serializable]
		private struct SetOfSpawnedObjects {
			public GameObject setOfGameObjects;
			public TypeOfSpawnedObjects typeOfSpawnedObjects;
		}

		[SerializeField] private GameObject player;
		[SerializeField] private float zSpawnPosition;
		[SerializeField] private float xSpawnPositionOffsetFromPlayer;
		[SerializeField] private float delayOfCheckTriggerSpawn = 0.5f;
		[SerializeField] private float xPlayerTriggerDifference;
		[SerializeField] private float xDistanceToClean = 10;
		[SerializeField] private SetOfSpawnedObjects[] setOfObjects;

		// The minimum health difference with player which random block should have
		private const int BlockHealthDifferenceWithPlayer = 1;

		// Chance from 0 to 100
		private const int chanceToDestroyRandomBlocks = 60;
		private const int chanceToDestroyHealers = 70;

		/// <summary>
		/// Multiplier where 1 - default, 3 - multiply by 3
		/// This range for random which multiply by playerHealth
		/// </summary>
		private readonly (float, float) _multiplierStaticBlocksHp = (0.4f, 2f);

		private readonly (float, float) _multiplierRandomBlocksHp = (0.2f, 2f);
		private readonly (float, float) _multiplierHealerHp = (0.2f, 1.6f);

		private readonly WaitForSeconds _checkPlayerInDestroyTrigger = new WaitForSeconds(1f);
		private readonly System.Random _rand = new System.Random();
		private IEnumerator<WaitForSeconds> _levelGeneratorCoroutine;

		private void Awake() {
			if (!player) {
				player = GameObject.FindGameObjectWithTag("Player");
			}
		}

		private void Start() {
			_levelGeneratorCoroutine = GenerateLevel();
			StartCoroutine(_levelGeneratorCoroutine);
		}

		private void OnDestroy() {
			StopCoroutine(_levelGeneratorCoroutine);
		}

		private IEnumerator<WaitForSeconds> GenerateLevel() {
			var playerLastPositionX = player.transform.position.x;

			for (var i = 0;; i = (i + 1) % setOfObjects.Length) {
				while (playerLastPositionX + xPlayerTriggerDifference > player.transform.position.x) {
					yield return new WaitForSeconds(delayOfCheckTriggerSpawn);
				}

				playerLastPositionX = player.transform.position.x;
				SpawnObject(setOfObjects[i].setOfGameObjects, setOfObjects[i].typeOfSpawnedObjects);
				yield return new WaitForSeconds(delayOfCheckTriggerSpawn);
			}
		}

		private void SpawnObject(GameObject spawnedObject, TypeOfSpawnedObjects typeOfSpawned) {
			var newSetOfBlocks = SpawnObjectClone(spawnedObject);
			SetPosition(ref newSetOfBlocks);
			switch (typeOfSpawned) {
				case TypeOfSpawnedObjects.NOT_RANDOM_BLOCKS:
					SetHealthToChildren(ref newSetOfBlocks, _multiplierStaticBlocksHp);
					break;
				case TypeOfSpawnedObjects.RANDOM_BLOCKS:
					RandomDeleteChildren(ref newSetOfBlocks, chanceToDestroyRandomBlocks);
					SetHealthToChildren(ref newSetOfBlocks, _multiplierRandomBlocksHp);
					break;
				case TypeOfSpawnedObjects.RANDOM_HEALER:
					RandomDeleteChildren(ref newSetOfBlocks, chanceToDestroyHealers);
					SetHealthToChildren(ref newSetOfBlocks, _multiplierHealerHp);
					break;
				case TypeOfSpawnedObjects.NONE:
					throw new ArgumentException("TypeOfSpawnedObjects.NONE is getting");
				default:
					throw new ArgumentException("Default is getting");
			}

			StartCoroutine(DestroyAfterPlayer(newSetOfBlocks));
		}

		private Vector3 GetSpawnPosition() {
			var spawnPosition = player.transform.position;
			spawnPosition.x += xSpawnPositionOffsetFromPlayer;
			spawnPosition.z = zSpawnPosition;
			return spawnPosition;
		}

		private IEnumerator<WaitForSeconds> DestroyAfterPlayer(GameObject destroyedObject) {
			while (destroyedObject.transform.position.x + xDistanceToClean > player.transform.position.x) {
				yield return _checkPlayerInDestroyTrigger;
			}

			Destroy(destroyedObject);
		}

		private GameObject SpawnObjectClone(GameObject spawnedObject) {
			var objectClone = Instantiate(spawnedObject);
			return objectClone;
		}

		private void SetPosition(ref GameObject currentObject) {
			currentObject.transform.position = GetSpawnPosition();
		}

		private void RandomDeleteChildren(ref GameObject newSetOfBlocks, int chanceToDestroy) {
			var rand = new System.Random();

			const int minNumber = 0;
			const int maxNumber = 101;
			if (chanceToDestroy < minNumber || maxNumber < chanceToDestroy) {
				throw new ArgumentOutOfRangeException(nameof(chanceToDestroy));
			}

			for (var indexOfChild = 0; indexOfChild < newSetOfBlocks.transform.childCount; indexOfChild++) {
				if (rand.Next(minNumber, maxNumber) < chanceToDestroy) {
					Destroy(newSetOfBlocks.transform.GetChild(indexOfChild).gameObject);
				}
			}
		}

		private void SetHealthToChildren(ref GameObject newSetOfBlocks, (float, float) multiplierHP) {
			var playerHealth = (int) player.GetComponent<HealthComponent>().Health;
			var healthOfBlocks = SetRandomHealthToBlocks(newSetOfBlocks, playerHealth, multiplierHP);
			SetRandomBlockHealth(ref healthOfBlocks, playerHealth - BlockHealthDifferenceWithPlayer);
		}

		private HealthComponent[] SetRandomHealthToBlocks(GameObject newSetOfBlocks, int baseHealth,
			(float, float) multiplierHP) {
			var healthOfBlocks = newSetOfBlocks.GetComponentsInChildren<HealthComponent>();
			foreach (var block in healthOfBlocks) {
				block.Health = Mathf.Round(baseHealth *
					_rand.Next((int) (multiplierHP.Item1 * 100), (int) (multiplierHP.Item2 * 100)) / 100f);
			}

			return healthOfBlocks;
		}

		// Set one of block hp less than on the player because it's the ability to play
		private void SetRandomBlockHealth(ref HealthComponent[] objectsHealth, int newHealth) {
			if (objectsHealth.Length != 0) {
				var indexOfPlayerHPBlock = _rand.Next(0, objectsHealth.Length);
				objectsHealth[indexOfPlayerHPBlock].Health = newHealth;
			}
		}
	}
}