using UnityEngine;

public class MapUnderPlayer : MonoBehaviour
{
    [Header("Object to follow")]
    [SerializeField] private GameObject _player;
    [Header("Configuration")]
    [Tooltip("The max size of the current gameobject")]
    [SerializeField] private const int _size = 100;

    [Tooltip("We need one more platform forward for nonvisible teleporation of this object")]
    [SerializeField] private bool _isNeedOneMorePlatform = false;

    private void Awake() {
        if (_isNeedOneMorePlatform) {
            _isNeedOneMorePlatform = false;
            GameObject cloneOfObject = Instantiate(gameObject);
            int defaultXOffset = _size;
            Move(cloneOfObject.transform, defaultXOffset);
        }
    }

    void FixedUpdate()
    {
        if (_player.transform.position.x > transform.position.x + _size) {
            int xAxisOffset = _size * 2;
            Move(transform, xAxisOffset);
        }
    }

    private void Move(Transform transformToMove, int xAxisOffset) {
        transformToMove.Translate(xAxisOffset, 0, 0);
    }
}
