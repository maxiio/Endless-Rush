using UnityEngine;

public class RespawnComponent : MonoBehaviour
{
    [SerializeField] private bool _isSpawnPosition;
    [SerializeField] private Vector3 _spawnPosition;

    private void Awake() {
        _spawnPosition = gameObject.transform.position;
    }

    public void Respawn() {
        gameObject.transform.position = _spawnPosition;
    }
}
