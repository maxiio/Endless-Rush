using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;

    [Header("Restart option")]
    [SerializeField] private bool _IsReloadScene = false;


    private float _defaultTimeScale = 1;

    public enum Request {
        NULL,
        RESTART,
        REVIVEPLAYER,
        PAUSE,
        UNPAUSE,
        EXIT
    }

    private void Awake() {
        _defaultTimeScale = Time.timeScale;
        ButtonAction.RequestedAction += RequestHandler;
    }

    private void OnDestroy() {
        ButtonAction.RequestedAction -= RequestHandler;
    }

    private void RequestHandler(object sender, Request request) {
        switch (request) {
            case Request.NULL:
                throw new Exception("Call the NULL switch");
            case Request.RESTART:
                Restart();
                break;
            case Request.REVIVEPLAYER:
                RevivePlayer();
                break;
            case Request.PAUSE:
                Pause();
                break;
            case Request.UNPAUSE:
                UnPause();
                break;
            case Request.EXIT:
                Exit();
                break;
            default:
                throw new Exception("Call the default switch");
        }
    }

    private void Restart() {
        if (_IsReloadScene) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            foreach (var gameObject in _gameObjects) {
                gameObject.GetComponent<RespawnComponent>()?.Respawn();
                gameObject.GetComponent<HealthComponent>()?.SetDefaultHealth(gameObject);
            }
        }
    }

    private void RevivePlayer() {
        throw new NotImplementedException();
    }

    private void Pause() {
        Time.timeScale = 0;
    }

    private void UnPause() {
        Time.timeScale = _defaultTimeScale;
    }

    private static void Exit() {
        Application.Quit(0);
    }
}
