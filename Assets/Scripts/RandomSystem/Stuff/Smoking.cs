using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoking : BasicStuff {
	public GameObject smokingPic;

	// 生成抽烟 slider 并且减少压力值
	public override void dealWithEvent(HumanStuff playerStuff) {
		if (_player.hands != HandsState.none) _tips.setTips("手上还有东西，不能抽烟");
		smokingPic.SetActive(true);
		_player.gameObject.SetActive(false);
		base.dealWithEvent(playerStuff);
	}

	public override void finished (int _playerID) {
		base.finished(_playerID);
		_tips.setTips("压力降低了");
		_player.gameObject.SetActive(true);
		smokingPic.SetActive(false);
		_player.pressure += pressureValue;
	}
}
