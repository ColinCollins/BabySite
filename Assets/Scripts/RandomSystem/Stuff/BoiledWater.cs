using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 水池事件，需要分阶段. 非主动触发事件要是想执行 主动事件流程，就需要手动的增删 TimeLine 和 slider
public class BoiledWater : BasicStuff {
	// 0 none, 1 hot water
	private int _schedule = 0;
	// 等待水凉的时间
	public float waitingTime;
	public float waterCoodTime;

	public override void init(StuffEventSystem eventSystem, HumanSystem player)
	{
		base.init(eventSystem, player);
		_waitingTime = waitingTime;
	}

	public override void waitBroken(HumanStuff playerStuff)
	{
		if (_schedule == 0)  return;
		_player.state = HumanState.isWorking;
		playerStuff.requireTime = requireTime;
		_eventSystem.eventClear(ID);
		dealWithEvent(playerStuff);
		_eventState = EventState.Stop;
	}

	public override void dealWithEvent(HumanStuff playerStuff)
	{
		// 判断当前 scheuld 
		if (_schedule == 0)
		{
			_tips.setTips("开始烧水");
			waitStart();
			_eventSystem.InitNewSlider(this);
		}
		else
		{
			_eventSystem.InitNewSlider(playerStuff);
			playerStuff.finishedEvent = finished;
		}
	}

	public override void finished(int _playerID)
	{
		base.finished(_playerID);
		_player.state = HumanState.isIdle;
		_player.hands = HandsState.keepMilk;
		_schedule = 0;
	}

	public override void waitFinished() {
		if (_schedule == 0)
		{
			_schedule = 1;
			_waitingTime = waterCoodTime;
			_tips.setTips("水开了");
			// 移除本次记录
			_eventSystem.eventClear(ID);
			// 开始新的计时
			_eventSystem.InitNewSlider(this);
		}
		else {
			_tips.setTips("水凉了");
			_schedule = 0;
			_waitingTime = waitingTime;
			_eventSystem.eventClear(ID);
		}
	}
}
