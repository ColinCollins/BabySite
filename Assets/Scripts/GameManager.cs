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
	private TipsManager _tips;

	public GameObject heartHandle;
    public GameObject pressureHandle;
    public GameObject turnTable;
	public GameObject gameOverPic;

	public float maxPressureValue = 0;
	public float maxHeartValue = 0;
    // turnTable children
    private GameObject pointer;
    private GameObject background;

    private static float _heartValue = 0;
    private static float _pressureValue = 0;
    private static float _curHeartValue = 0;
    private static float _curPressureValue = 0;

	private Image _pressureBarHandle;
	private Image _heartHandle;

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

		_pressureBarHandle = pressureHandle.GetComponent<Image>();
		_heartHandle = heartHandle.GetComponent<Image>();
		_tips = TipsManager.getInstance();
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

    public void setPressureValue(float value) {
        _pressureValue = value;
    }

    public void SliderEffect() {
        if (_curHeartValue != _heartValue) {
            _curHeartValue = CaculateSliderValue(_curHeartValue, _heartValue);
			_heartHandle.fillAmount = _curHeartValue / maxHeartValue;
        }
        if (_curPressureValue != _pressureValue) {
            _curPressureValue = CaculateSliderValue(_curPressureValue, _pressureValue);
			_pressureBarHandle.fillAmount = _curPressureValue / maxPressureValue;
			//Debug.Log("fillAmount:" + _pressureBarHandle.fillAmount);
        }
    }

    public void TurnGame(HumanSystem player)
    {
		pointer.transform.Rotate(Vector3.forward);
		float z = Mathf.Abs(pointer.transform.rotation.z % 360);
		if (Input.GetMouseButtonDown(0))
		{
			if (z >= 0.9 || z <= 1)
			{
				player.heart += 33;
				pointer.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(-1, 1));
				player.state = HumanState.isIdle;
				_tips.setTips("妻子心情变好了");
			}
			turnTable.gameObject.transform.localPosition = new Vector3(99999, 0, 0);
			player.isTurnGame = false;
		}
	}
	public void startTurnGame (HumanSystem player) {
		player.isTurnGame = true;
		turnTable.transform.localPosition = new Vector3(-440, -255, 0);
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
		if (State == GameState.Over && _heartValue == 100)
		{
			SceneManager.LoadScene("endScene");
		}
		else {
			gameOverPic.SetActive(true);
			Time.timeScale = 0;
		}
    }
    // a slider effect
    private float CaculateSliderValue(float curValue, float MaxValue) {
        curValue += Time.deltaTime * 10 * (MaxValue - curValue);
		if (curValue >= MaxValue) {
            curValue = MaxValue;
        }
        return curValue;
    }
    
}
