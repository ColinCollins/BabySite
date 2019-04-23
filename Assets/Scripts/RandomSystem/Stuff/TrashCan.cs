using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : BasicStuff {

	public override void dealWithEvent(HumanStuff playerStuff)
	{
		if (_player.hands == HandsState.none) _tips.setTips("手上没东西可以丢");
		base.dealWithEvent(playerStuff);
	}

	public override void finished(int _playerID) {
		if (_player.hands == HandsState.keepDirtyDiaper) _tips.setTips("丢弃脏尿布");
		else if (_player.hands == HandsState.keepDiaper) _tips.setTips("丢弃尿布");
		else _tips.setTips("丢弃奶瓶");
		base.finished(_playerID);
		_player.pressure += pressureValue;
	}
}
