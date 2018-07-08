using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
    Start,
    Pause,
    Play,
    Over
}

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    // get the game state event
    private delegate void CallFunc();
    private CallFunc callback;
    private GameState _gameState = GameState.Start;

    public GameObject heartBar;
    public GameObject patientBar;
    public GameObject turnTable;
    // turnTable children
    private GameObject pointer;
    private GameObject background;

    private static float _heartValue = 0;
    private static float _patientValue = 0;
    private static float _curHeartValue = 0;
    private static float _curPatientValue = 0;

    public GameState State {
        get{
            return _gameState;
        }
        set {
            _gameState = value;
        }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
    }

    void Start() {
        foreach (Transform tran in turnTable.GetComponentsInChildren<Transform>())
        {
            if (tran.name == "pointer")
            {
                pointer = tran.gameObject;
            }
            else if (tran.name == "background")
            {
                background = tran.gameObject;
            }
        }
    }

    public static GameManager getInstance() {
        if (_instance != null)
        {
            return _instance;
        }
        else {
            _instance = new GameManager();
          //  Debug.LogError("GameManager get Lost!");
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
        SliderEffect();
    }

    public void setHeartValue(float value) {
        _heartValue = value;
    }

    public void setPatientValue(float value) {
        _patientValue = value;
    }

    public void SliderEffect() {
        if (_curHeartValue != _heartValue) {
            Image heartHandle = heartBar.GetComponentInChildren<Image>();
            _curHeartValue = CaculateSliderValue(_curHeartValue, _heartValue);
            heartHandle.fillAmount = _curHeartValue / 100.0f;
        }
        if (_curPatientValue != _patientValue) {
            
            Image patientHandle = patientBar.GetComponentInChildren<Image>();
            Debug.Log(patientHandle.gameObject.name);
            _curPatientValue = CaculateSliderValue(_curPatientValue, _patientValue);
            patientHandle.fillAmount = _curPatientValue / 50.0f;
        }
    }

    public void TurnGame(HumanSystem person)
    {
        if (person.isRotate)
        {
            pointer.transform.Rotate(Vector3.forward);
            float z = Mathf.Abs(pointer.transform.rotation.z % 360);
            if (Input.GetMouseButtonDown(0))
            {
                person.isRotate = false;  
                if (z >= 0.9 || z <= 1)
                {
                    person.heart += 33;
                    pointer.transform.eulerAngles =new Vector3(0, 0, UnityEngine.Random.Range(-1, 1));
                }
                turnTable.gameObject.transform.localPosition = new Vector3(99999, 0, 0);
            }
        }
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
    // set the GameOver logic
    public void GameOver() {
        if (State == GameState.Over) {
            SceneManager.LoadScene("endScene");
        } 
    }
    // a slider effect
    private float CaculateSliderValue(float curValue, float MaxValue) {
        var value = Time.deltaTime * 10;
        curValue += value;
        if (curValue >= MaxValue) {
            curValue = MaxValue;
        }
        return curValue;
    }
    
}
