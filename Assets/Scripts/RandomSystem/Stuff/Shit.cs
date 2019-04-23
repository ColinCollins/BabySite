using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : BasicStuff {

	public GameObject shitPic;
	public float waitingTime = 0;

	public override void init(StuffEventSystem eventSystem, HumanSystem player)
	{
		base.init(eventSystem, player);
		_waitingTime = waitingTime;
	}

	public override void waitStart() {
		base.waitStart();
		shitPic.SetActive(true);
	}

	public override void waitBroken(HumanStuff playerStuff) {
		if (_player.hands != HandsState.keepDiaper)
		{
			_tips.setTips("请去拿尿布");
			// 播放提示音效
			return;
		}
		base.waitBroken(playerStuff);
	}

	public override void waitFinished() {
		base.waitFinished();
		_tips.setTips("宝宝心情很差");
		shitPic.SetActive(false);
	}

	public override void dealWithEvent(HumanStuff playerStuff) {
		base.dealWithEvent(playerStuff);
	}

	public override void finished(int _playerID) {
		base.finished(_playerID);
		_tips.setTips("更换尿布");
		shitPic.SetActive(false);
		_player.hands = HandsState.keepDirtyDiaper;
	}
}
