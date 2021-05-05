using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    [Range(0, size / 2)]
    private float moveX;

    const int size = 100;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x > transform.position.x + size / 2 + moveX) {
            Move();
        }
    }

    private void Move() {
        transform.Translate(size * 2, 0, 0);
    }
}
