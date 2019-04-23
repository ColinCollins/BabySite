using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventState {
	Stop,
	Waiting
}

public class BasicStuff : MonoBehaviour {
	public int ID = 0;			// 当前物体的唯一标识
	public float pressureValue = 0;  // 影响角色的压力值

	public float requireTime = 0;    // 处理当前事件所需要的时间
	protected float _waitingTime = 0; // 当前物理等待处理时间

	protected StuffEventSystem _eventSystem;
	protected TimeLine _timer;
	protected HumanSystem _player;
	protected EventState _eventState;
	protected TipsManager _tips;

	protected AudioSystem _audio;
	public bool isTool = false;
	public bool isOrder = false;	// 因为婴儿床的事件是叠加事件，因此需要 active = false 来控制
	public bool isTel = false;		// 用于标记特殊物体 电话 

	public virtual void init(StuffEventSystem eventSystem, HumanSystem player) {
		_player = player;
		_eventSystem = eventSystem;
		_timer = TimeLine.getInstance();
		_eventState = EventState.Stop;
		_audio = AudioSystem.getInstance();
		_tips = TipsManager.getInstance();
	}
	public void setWaitTime(float value) {
		_waitingTime = value;
	}
	public float getWaitTime() {
		return _waitingTime;
	}

	public EventState GetEventState() {
		return _eventState;
	}

	// 主动事件开始等待
	public virtual void waitStart() {
		//Debug.Log("Start Waiting");
		_eventState = EventState.Waiting;
	}
	// 主动事件结束等待
	public virtual void waitFinished() {
		//Debug.Log("Waiting Finished");
		_eventState = EventState.Stop;
		_player.pressure += pressureValue;
	}
	// 主动事件中断等待
	public virtual void waitBroken(HumanStuff playerStuff) {
		dealWithEvent(playerStuff);
		_eventState = EventState.Stop;
		_eventSystem.eventClear(ID);
		_eventSystem.fullBackTheEventStuff(ID);
	}
	// 开始处理事件
	public virtual void dealWithEvent(HumanStuff playerStuff) {
		//Debug.Log("deal With Event");
		// 设置消耗时间
		playerStuff.requireTime = requireTime;
		// 设置委托
		playerStuff.finishedEvent = finished;
		// 生成新的 slider
		_eventSystem.InitNewSlider(playerStuff);
		// 人物当前不可移动
		_player.state = HumanState.isWorking;
	}
	// 处理事件结束
	public virtual void finished(int _playerID) {
		//Debug.Log("deal With Finish");
		// 人物可以移动
		_player.state = HumanState.isIdle;
		_player.hands = HandsState.none;
	} 
}
