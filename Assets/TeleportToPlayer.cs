using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPlayer : MonoBehaviour
{
    [SerializeField] private Transform _objectToTeleport;
    [SerializeField] private float _distanceToTriggerTeleport;

    private Vector3 _positionToTeleport;
    private Vector3 _objectLastPosition;

    private void Start() {
        _positionToTeleport = _objectToTeleport.position;
        // Difference with object to teleport
        _positionToTeleport.y -= 0.5f;

        _objectLastPosition = _objectToTeleport.position;
    }

    private void TeleportToObject() {
        _positionToTeleport.x = _objectToTeleport.position.x;
        gameObject.transform.position = _positionToTeleport;
    }

    private void FixedUpdate()
    {
        if (_objectLastPosition.x + _distanceToTriggerTeleport < _objectToTeleport.position.x) {
            TeleportToObject();
            _objectLastPosition = _objectToTeleport.position;
        }
    }
}
