using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanStuff : BasicStuff {

	// 委托事件，处理结束时的事件内容
	public delegate void Finished(int _playerID);
	public Finished finishedEvent;

	public override void init(StuffEventSystem eventSystem, HumanSystem player) {
		base.init(eventSystem, player);
	}

	public override void dealWithEvent(HumanStuff _playerStuff) { }
	public override void finished(int _playerID) { }
}
