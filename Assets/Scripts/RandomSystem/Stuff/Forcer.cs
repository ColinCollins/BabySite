using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcer : BasicStuff {

	public override void dealWithEvent(HumanStuff playerStuff)
	{
		if (_player.hands != HandsState.none) {
			_tips.setTips("请放下手里的东西再来拿尿布");
			return;
		}
		base.dealWithEvent(playerStuff);
	}

	public override void finished(int _playerID)
	{
		_tips.setTips("拿起尿布");
		base.finished(_playerID);
		_player.hands = HandsState.keepDiaper;
	}
}
