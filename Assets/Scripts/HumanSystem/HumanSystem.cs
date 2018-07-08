using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum HumanState { 
    isWalking,
    isSlowing, // keep the speed down
    isWorking,
    isIdle
}

public enum HandsState { 
    keepMilk,
    keepDiaper,
    none,
}

public class HumanSystem : MonoBehaviour {

    private MoveController _controller = null;
    private SolveEventSystem _seSystem = null;
    private HumanAnimation _animSystem = null;
    private GameManager _gameManager = null;
    [HideInInspector]
    public bool isTurnGame = false;
    public bool isRotate = false;

    #region Prototype
    private float _patient = 0;
    public float patient {
        get {
            return _patient;
        }
        set {
            _patient = value;
            if (_patient >= 50 && _gameManager != null) {
                _patient = 50;
                _gameManager.setPatientValue(_patient);
                _gameManager.State = GameState.Over;
            }
            _gameManager.setPatientValue(_patient);
        }
    }
    private float _heart = 0;
    public float heart {
        get {
            return _heart;
        }
        set {
            _heart = value;
            if (_heart >= 99 && _gameManager != null)
            {
                _heart = 100;
                _gameManager.setHeartValue(_heart);
                _gameManager.State = GameState.Over;
            }
            _gameManager.setHeartValue(_heart);
        }
    }

    private Direction _direction = Direction.Right;
    public Direction direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    private HumanState _state = HumanState.isIdle;
    public HumanState state
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }

    private HandsState _hands = HandsState.none;
    public HandsState hands {
        get {
            return _hands;
        }
        set {
            _hands = value;
        }
    }
    #endregion

	// Use this for initialization
	void Start () {
        _controller = new MoveController(this);
        _seSystem = new SolveEventSystem(this);
        _animSystem = new HumanAnimation(this);
        _gameManager = GameManager.getInstance();
        Debug.Log(_gameManager);
	}

	// Update is called once per frame
	void Update () {
        if (_controller == null) {
            Debug.LogError("Controller get Lost!");
            return;
        }
        _controller.WalkState();
        _animSystem.PlayAction();
        _seSystem.getEventCollision();

        if (isTurnGame)
        {
            _gameManager.TurnGame(this);
        }

	}
    // keep the sprite on common
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall") {
            var wall = other.transform.parent.GetComponent<SpriteRenderer>();
            wall.color = new Color(255, 255, 255, 0.4f);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall") {
            var wall = other.transform.parent.GetComponent<SpriteRenderer>();
            wall.color = new Color(255, 255, 255, 1);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, getTargetPosition());
    }
    // use to draw the line
    public Vector2 getTargetPosition()
    {
        var rayline = 3;
        Vector2 pos = transform.position;
        Vector2 tarPos = Vector2.zero;
        Direction dic = direction;
        //Debug.Log(dic);
        switch (dic)
        {
            case Direction.Left:
                tarPos = new Vector2(pos.x - rayline, pos.y);
                break;
            case Direction.Up:
                tarPos = new Vector2(pos.x, pos.y + rayline);
                break;
            case Direction.Right:
                tarPos = new Vector2(pos.x + rayline, pos.y);
                break;
            case Direction.Down:
                tarPos = new Vector2(pos.x, pos.y - rayline);
                break;
        }
        return tarPos;
    }
}
