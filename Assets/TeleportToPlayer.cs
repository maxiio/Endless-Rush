using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPlayer : MonoBehaviour
{
    [SerializeField] private Transform _objectToTeleport;
    Vector3 _positionToTeleport;

    private void Start() {
        _positionToTeleport = _objectToTeleport.position;
        // Difference with object to teleport
        _positionToTeleport.y -= 0.1f;
    }

    private void FixedUpdate()
    {
        _positionToTeleport.x = _objectToTeleport.position.x;
        gameObject.transform.position = _positionToTeleport;
    }
}
