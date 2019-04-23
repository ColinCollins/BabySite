using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// baby 需要玩耍事件
public class Playing : BasicStuff {

	public GameObject playingPic;
	public float waitingTime = 0;

	public override void init(StuffEventSystem eventSystem, HumanSystem player) {
		base.init(eventSystem, player);
		_waitingTime = waitingTime;
	}

	public override void waitStart() {
		base.waitStart();
		playingPic.SetActive(true);
	}

	public override void waitBroken(HumanStuff playerStuff) {
		if (_player.hands != HandsState.none)
		{
			_tips.setTips("手里还有东西就不能接触宝宝");
			return;
		}
		base.waitBroken(playerStuff);
	}

	public override void waitFinished() {
		base.waitFinished();
		_tips.setTips("宝宝心情很差");
		playingPic.SetActive(false);
	}

	public override void dealWithEvent(HumanStuff playerStuff)
	{
		base.dealWithEvent(playerStuff);
	}

	public override void finished(int _playerID) {
		base.finished(_playerID);
		_tips.setTips("宝宝心情很爽~");
		playingPic.SetActive(false);
	}
}
