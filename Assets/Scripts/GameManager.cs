using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Start,
    Pause,
    Play,
    Over
}

public class GameManager : MonoBehaviour {

    private static GameManager _instance = new GameManager();
    private delegate void CallFunc();
    private CallFunc callback;
    private GameState _gameState = GameState.Start;
    public Canvas canvas;
    private float _heartvalue = 0;
    private float _patientvalue = 0;

    public GameState State {
        get{
            return _gameState;
        }
        set {
            _gameState = value;
        }
    }

    public static GameManager getInstance() {
        if (_instance != null)
        {
            return _instance;
        }
        else { 
            Debug.LogError("GameManager get Lost!");
        }
        return null;
    }

    private void ListenerGameState() {
        switch (State) { 
            case GameState.Start:
                callback = GameStart;
                break;
            case GameState.Play:
                callback = GamePlay;
                break;
            case GameState.Pause:
                callback = GamePause;
                break;
            case GameState.Over:
                callback = GameOver;
                break;
        }
        callback();
    }

    void Update() {
        ListenerGameState();
    }

    public void GameStart() { 
           
    }

    public void GamePlay() { 
    
    }
    public void GamePause() {
        State = GameState.Pause;
        Time.timeScale = 0;
    }
    public void GameResume() { 
        
    }
    public void GameOver() { 
        
    }

    
}
