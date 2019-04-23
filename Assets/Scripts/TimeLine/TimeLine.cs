using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 用于控制时间线
public class TimeLine {
	private static TimeLine _timer = null;
	private float _curTime = 0.0f;

	private float _elapseTime = 0.0f; // 间隔时间
	private bool _triggerFlag = false;  // 判断是否可以触发事件的返回值
	private List<int> _timeIDList = new List<int>();
	private Dictionary<int, float> _sliderTimeList = new Dictionary<int, float>(); // 事件时间表

	public static TimeLine getInstance() {
		if (_timer == null) {
			_timer = new TimeLine();
		}
		return _timer;
	}

	// 准备触发事件
	public bool readyToTriggerEvent(float dt) {
		_elapseTime -= dt;
		if (_elapseTime < 0) {
			_elapseTime = 0;
			return true;
		}
		return false;
	}

	#region GetSet
	public void setElapseTime(float value) {
		_elapseTime = value;
		_triggerFlag = false;
	}

	public float getCurTime() {
		return _curTime;
	}

	public Dictionary<int, float> getSlidetDic() {
		return _sliderTimeList;
	}

	public List<int> getSliderIDList() {
		return _timeIDList;
	}

	public void setSliderTime(int ID, float timeLimit) {
		_sliderTimeList.Add(ID, timeLimit);
		_timeIDList.Add(ID);
	}
	#endregion
	// 计算时间
	public bool spendTime(float dt)
	{
		if (readyToTriggerEvent(dt))
		{
			Debug.Log("Event Trigger.");
			_triggerFlag = true;
		}

		_curTime += dt;
		foreach (int key in _timeIDList)
		{	// 物体数量是有限的
			if (_sliderTimeList[key] <= 0) _sliderTimeList[key] = 0;
			else _sliderTimeList[key] -= dt;
		}
		return _triggerFlag;
	}

	public void removeSliderTimeRec(int ID) {
		_sliderTimeList.Remove(ID);
		_timeIDList.Remove(ID);
	}
}
