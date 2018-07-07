using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanState { 
    isWalking,
    isSlowing, // keep the speed down
    isWorking,
    isIdle
}

public class HumanSystem : MonoBehaviour {

    private MoveController _controller = null;
    private SolveEventSystem _seSystem = null;
    private HumanAnimation _animSystem = null;
    private GameManager _gameManager = null;
    #region Prototype
    private float _patient = 0;
    public float patient {
        get {
            return _patient;
        }
        set {
            _patient = value;
            if (_patient >= 50 && _gameManager != null) {
                _gameManager.State = GameState.Over;
            }
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
    #endregion

	// Use this for initialization
	void Start () {
        _controller = new MoveController(this);
        _seSystem = new SolveEventSystem(this);
        _animSystem = new HumanAnimation(this);
        _gameManager = GameManager.getInstance();
	}

	// Update is called once per frame
	void Update () {
        if (_controller == null) {
            Debug.LogError("Controller get Lost!");
            return;
        }
        _controller.WalkState();
        _animSystem.PlayAction();

	}


}
