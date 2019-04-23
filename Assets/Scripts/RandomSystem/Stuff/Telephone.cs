using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : BasicStuff {

	public float waitingTime = 0;

	public override void init(StuffEventSystem eventSystem, HumanSystem player)
	{
		base.init(eventSystem, player);
		_waitingTime = waitingTime;
	}
	public override void waitStart()
	{
		base.waitStart();
		// 播放音效
		_audio.playTelePhone(true);
	}

	public override void waitFinished()
	{
		base.waitFinished();
		_audio.playTelePhone(false);
	}

	public override void waitBroken(HumanStuff playerStuff)
	{
		if (_player.hands != HandsState.none) {
			_tips.setTips("请放下手中的物品，再接电话");
			return;
		}
		base.waitBroken(playerStuff);
		_audio.playTelePhone(false);
		// 显示轮盘对象
		GameManager.getInstance().startTurnGame(_player);
	}

	public override void dealWithEvent(HumanStuff playerStuff)
	{
		base.dealWithEvent(playerStuff);
	}
	// 这里用 AOE 处理
	public override void finished(int _playerID){
		base.finished(_playerID);
	}
}
