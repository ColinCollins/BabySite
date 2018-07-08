using UnityEngine;
using System.Collections;
using System;

public delegate void CompleteEvent();
public delegate void UpdateEvent(float t);

public class Timer : MonoBehaviour
{
    bool _isLog = true;

    UpdateEvent updateEvent;

    CompleteEvent onCompleted;
    CompleteEvent onHalfHandle;

    float _timeTarget;   // 计时时间/

    float _timeStart;    // 开始计时时间/

    float _timeNow;     // 现在时间/

    float _offsetTime;   // 计时偏差/

    bool _isTimer;       // 是否开始计时/

    bool _isDestory = true;     // 计时结束后是否销毁/

    bool _isEnd;         // 计时是否结束/

    bool _isIgnoreTimeScale = true;  // 是否忽略时间速率

    bool _isRepeate;
    bool _isRandom;
    int _min;
    int _max;
    bool _isHalfHandle;

    float Time_
    {
        get { return _isIgnoreTimeScale ? Time.realtimeSinceStartup : Time.time; }
    }
    public bool IsEnd
    {
        get { return _isEnd; }
    }
    float _now;

    // Update is called once per frame
    void Update()
    {
        if (_isTimer && !IsEnd)
        {
            _timeNow = Time_ - _offsetTime;
            _now = _timeNow - _timeStart;
            if (updateEvent != null)
                updateEvent(Mathf.Clamp01(_now / _timeTarget));
            if (_now > _timeTarget)
            {
                if (onCompleted != null)
                    onCompleted();
                if (!_isRepeate)
                    Destory();
                else
                    ReStartTimer();
            }
        }
    }
    
    public float GetLeftTime()
    {
        return Mathf.Clamp(_timeTarget - _now, 0, _timeTarget);
    }
    public float GetCurrentTime()
    {
        return Mathf.Clamp(_now, 0, _timeTarget);
    }
    void OnApplicationPause(bool isPause_)
    {
        if (isPause_)
        {
            PauseTimer();
        }
        else
        {
            ConnitueTimer();
        }
    }

    /// <summary>
    /// 计时结束
    /// </summary>
    public void Destory()
    {
        _isTimer = false;
        _isEnd = true;
        if (_isDestory)
        {
            Debug.Log("destory");
            Destroy(gameObject);
        }
    }
    float _pauseTime;
    /// <summary>
    /// 暂停计时
    /// </summary>
    public void PauseTimer()
    {
        if (_isEnd)
        {
            if (_isLog) Debug.LogWarning("计时已经结束！");
        }
        else
        {
            if (_isTimer)
            {
                _isTimer = false;
                _pauseTime = Time_;
            }
        }
    }
    /// <summary>
    /// 继续计时
    /// </summary>
    public void ConnitueTimer()
    {
        if (_isEnd)
        {
            if (_isLog) Debug.LogWarning("计时已经结束！请从新计时！");
        }
        else
        {
            if (!_isTimer)
            {
                _offsetTime += (Time_ - _pauseTime);
                _isTimer = true;
            }
        }
    }
    public void ReStartTimer()
    {
        if (_isRandom)
        {
            _timeTarget = UnityEngine.Random.Range(_min,_max+1);
        }
        _timeStart = Time_;
        _offsetTime = 0;
    }

    public void ChangeTargetTime(float time)
    {
        _timeTarget += time;
    }
    /// <summary>
    /// 开始计时 : 
    /// @param float: random time set.
    /// @param CompleteEvent []: when complete event happen.
    /// @param UpdateEvent []: when update the value could be execute. 
    /// @param bool: is get timescale.
    /// @param bool: is repeate.
    /// @param bool: is Random if you restart or loop you timing.
    /// @param int min 
    /// @param int max
    /// @param bool is destroy when finished the timing
    /// </summary>
    public void StartTiming(float time, CompleteEvent onCompleted_ = null, UpdateEvent update = null, bool isIgnoreTimeScale_ = true, bool isRepeate_ = false, bool isRandom = false, int min = 0, int max = 0, bool isDestory_ = true)
    {
        _timeTarget = time;
        if (onCompleted_ != null)
            onCompleted = onCompleted_;
        if (update != null)
            updateEvent = update;
        _isDestory = isDestory_;
        _isIgnoreTimeScale = isIgnoreTimeScale_;
        _isRepeate = isRepeate_;
        _isRandom = isRandom;
        _min = min;
        _max = max;

        _timeStart = Time_;
        _offsetTime = 0;
        _isEnd = false;
        _isTimer = true;
    }
    /// <summary>
    /// 创建计时器:名字
    /// </summary>
    public static Timer CreateTimer(string gobjName = "Timer")
    {
        GameObject g = new GameObject(gobjName);
        Timer timer = g.AddComponent<Timer>();
        return timer;
    }

}
