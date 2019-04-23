using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { 
    Left,
    Up,
    Right,
    Down
}

public class MoveController{
    // use to controller the player move an
    private HumanSystem _person = null;
    private float max_speed = 10;
    private float cur_speed = 0f;
    private float lerp_rate = 50;

    public MoveController(HumanSystem person) {
        _person = person;
    }

    public void WalkState() {
        if (_person == null)
        {
            Debug.LogError("Controller get error!");
            return;
        }
        if (cur_speed == 0)
        {
            _person.state = HumanState.isIdle;
        }

        #region Walking Controller
        if (Input.GetKey(KeyCode.A) && _person.state == HumanState.isIdle) {
            _person.state = HumanState.isWalking;
            _person.direction = Direction.Left;
        }

        if (Input.GetKey(KeyCode.D) && _person.state == HumanState.isIdle)
        {
            _person.state = HumanState.isWalking;
            _person.direction = Direction.Right;
        }

        if (Input.GetKey(KeyCode.W) && _person.state == HumanState.isIdle)
        {
            _person.state = HumanState.isWalking;
            _person.direction = Direction.Up;
        }

        if (Input.GetKey(KeyCode.S) && _person.state == HumanState.isIdle)
        {
            _person.state = HumanState.isWalking;
            _person.direction = Direction.Down;
        }

        if (Input.GetKeyUp(KeyCode.A) && _person.state == HumanState.isWalking) {
            _person.state = HumanState.isSlowing;
        }
        if (Input.GetKeyUp(KeyCode.W) && _person.state == HumanState.isWalking) {
            _person.state = HumanState.isSlowing;
        }
        if (Input.GetKeyUp(KeyCode.D) && _person.state == HumanState.isWalking) {
            _person.state = HumanState.isSlowing;
        }
        if (Input.GetKeyUp(KeyCode.S) && _person.state == HumanState.isWalking) {
            _person.state = HumanState.isSlowing;
        }
        #endregion

        if (_person.state != HumanState.isIdle && _person.state != HumanState.isWorking)
            Moving();
    }

    private float AddLerpSpeed(float speed, float max_speed) {
        speed += Time.deltaTime * lerp_rate;
        if (speed > max_speed) {
            speed = max_speed;
        }
        return speed;
    }

    private float CutLerpSpeed(float speed, float min_speed) {
        speed -= Time.deltaTime * lerp_rate;
        if (speed < min_speed)
        {
            speed = min_speed;
        }
        return speed;
    }

    private void Moving() {
        if (_person.state == HumanState.isWalking) {
            cur_speed = AddLerpSpeed(cur_speed, max_speed);
        }
        else if (_person.state == HumanState.isSlowing) {
            cur_speed = CutLerpSpeed(cur_speed, 0);
        }
        switch (_person.direction) { 
            case Direction.Left:
                _person.transform.Translate(new Vector3(-cur_speed * Time.deltaTime, 0, 0));
                break;
            case Direction.Up:
                _person.transform.Translate(new Vector3(0, cur_speed * Time.deltaTime, 0));
                break;
            case Direction.Right:
                _person.transform.Translate(new Vector3(cur_speed * Time.deltaTime, 0, 0));
                break;
            case Direction.Down:
                _person.transform.Translate(new Vector3(0, -cur_speed * Time.deltaTime, 0));
                break;
        }
    }
}
