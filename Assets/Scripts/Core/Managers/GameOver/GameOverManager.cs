using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Components.Health;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("GameObjects which send the dead notification")]
    [SerializeField] private GameObject[] _gameObjects;

    [Header("Screen manager")]
    [SerializeField] private ScreenTransitionManager _screenManager;



    private void Awake() {
        foreach (GameObject gameObject in _gameObjects) {
            gameObject.GetComponent<HealthComponent>().IsDieEvent += GameOverSender;
        }
    }

    private void OnDestroy() {
        foreach (GameObject gameObject in _gameObjects) {
            // TO DO is destroyes
            //gameObject.GetComponent<HealthComponent>().IsDieEvent -= GameOverSender;
        }
    }

    private void GameOverSender(object sender, EventArgs e) {
        _screenManager.CallAction(ButtonAction.Actions.GAMEOVER);
    }
}
