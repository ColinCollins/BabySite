using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 主动事件必须含有一个 reuqire Time > 0 不然将无法重新加回队列

public class StuffEventSystem : MonoBehaviour {

	public List<BasicStuff> stuffList;      // 主动触发事件对象
	public List<BasicStuff> toolStuffList;  // 被动触发事件
	public HumanSystem player;
	public bool isTimeLimit = false;
	public float timePoint;
	public GameObject slider = null;

	#region private
	private int _timePointCount = 0;
	private TimeLine _timer = null;
	private HumanStuff _playerStuff;
	private Dictionary<int, float> _triggedEvent = new Dictionary<int, float>(); // 正在触发事件 + 等待时间
	private Dictionary<int, Slider> _sliderDic = new Dictionary<int, Slider>(); // 触发时间的对应 slider 对象
	private List<int> _postiveEventList = new List<int>();                      // 主动触发事件列表
	#endregion

	private void Start() {
		for (int i = 0; i < stuffList.Count; i++) {
			BasicStuff stuff = stuffList[i];
			stuff.init(this, player);
			if (stuff.isOrder)
			{
				stuff.gameObject.SetActive(false);
				_postiveEventList.Add(stuff.ID);
			}
			_triggedEvent.Add(stuff.ID, stuff.getWaitTime());
		}

		for (int i = 0; i < toolStuffList.Count; i++)
		{
			BasicStuff stuff = toolStuffList[i];
			stuff.init(this, player);
			_triggedEvent.Add(stuff.ID, stuff.requireTime);
		}

		_timer = TimeLine.getInstance();
		// 初始化时间点
		_timePointCount = 0;
		// 起始等待 3 s
		_timer.setElapseTime(3.0f);
		_playerStuff = player.getStuffComp();
		_triggedEvent.Add(_playerStuff.ID, _playerStuff.requireTime);
	}

	private void Update() {
		if (_timer.spendTime(Time.deltaTime)) {
			createRandomEvent();
		}
		reSetSliderValue();
	}

	private void createRandomEvent() {
		if (stuffList.Count <= 0) {
			Debug.LogWarning("StuffList is empty.");
			return;
		}
		// 选择事件
		Debug.Log("新事件生成");
		choiceStuff();
		#region reset the elapseTime
		// 重新设置事件生成间隔时间
		float elapse = (timePoint - _timer.getCurTime()) / 10;
		Debug.Log("elpase time: " + elapse);
		if (elapse <= 15.0f)
		{
			elapse = 15.0f;
		}
		_timer.setElapseTime(elapse);
		#endregion
	}
	#region choiceStuff 挑选随机事件触发
	private void choiceStuff() {
		if (_postiveEventList.Count <= 0) {
			Debug.Log("All stuffEvent has been trigged.");
			return;
		}

		int ID = getRandomEventID();
		// 移除对象物体列表
		dispatchTheEventStuff(ID);
		BasicStuff stuff = stuffList.Find(x => x.ID == ID);
		stuffStartWaiting(stuff);
	}
	#endregion
	#region InitNewSlider 生成新的 slider 在对应的事件物体上方
	public void InitNewSlider(BasicStuff stuff) {
		if (slider == null) Debug.Log("Slider GameObject lost");
		// 是否创建新的　Slider
		createNewSlider(stuff);
		// 创建新的 slider 对象
		int ID = stuff.ID;
		if (ID == _playerStuff.ID) {
			_timer.setSliderTime(ID, stuff.requireTime);
			_triggedEvent[ID] = _playerStuff.requireTime;
		}
		else {
			// 允许中间切入主动事件发生， 并且及时更新消耗最大事件
			if (_triggedEvent.ContainsKey(ID)) {
				_triggedEvent[ID] = stuff.getWaitTime();
			}
			_timer.setSliderTime(ID, stuff.getWaitTime());
		}
	}
	#endregion
	// 设置每个 slider 对应的百分比
	public void reSetSliderValue() {
		// 获取 _timeSliderDic
		Dictionary<int, float> sliderTimeDic = _timer.getSlidetDic();
		List<int> sliderIDList = _timer.getSliderIDList();
		if (_sliderDic.Count <= 0) return;

		for (int i = 0; i < sliderIDList.Count; i++) {
			int id = sliderIDList[i];
			Slider slider = _sliderDic[id];

			if (sliderTimeDic[id] > 0)
				slider.value = sliderTimeDic[id] / _triggedEvent[id];
			else if (!_postiveEventList.Contains(id)) {
				BasicStuff stuff = stuffList.Find(x => x.ID == id);
				if (id != _playerStuff.ID) {
					stuff.waitFinished();
					// 添加回会触发事件
					if (id > 100 && !stuff.isTool) fullBackTheEventStuff(id);
				}

				// 将非主动事件的清理权限交给 waitingFinished
				if ((id > 100 && !stuff.isTool) || id == _playerStuff.ID) {
					eventClear(id);
				}
			}
		}
	}

	private void createNewSlider(BasicStuff stuff) {
		int ID = stuff.ID;
		Vector3 parentPos = stuff.transform.position;
		if (_sliderDic.ContainsKey(ID))
		{
			// 重新激活
			_sliderDic[ID].gameObject.SetActive(true);
			_sliderDic[ID].transform.position = new Vector3(parentPos.x + 0.3f, parentPos.y + 2.0f, parentPos.z);
		}
		else
		{
			GameObject newSlider = Instantiate(slider);
			newSlider.transform.position = new Vector3(parentPos.x + 0.3f, parentPos.y + 2.0f, parentPos.z);
			_sliderDic.Add(stuff.ID, newSlider.GetComponent<Slider>());
		}
	}
	// 手动控制事件触发
	private void CustomEvnetTrigger() {
		// 每隔 70 s 必定触发
		int index = (int)_timer.getCurTime() / 50;
		if (index > _timePointCount) {
			Debug.Log("固定电话触发事件触发");
			_timePointCount++;
			BasicStuff tel = stuffList.Find(x => x.isTel);
			stuffStartWaiting(tel);
		}
	}

	private void stuffStartWaiting(BasicStuff stuff) {
		int ID = stuff.ID;
		float waitTime = stuff.getWaitTime();
			InitNewSlider(stuff);

				// 记录需要最大时间值
				if (!_triggedEvent.ContainsKey(ID))
				{
					_triggedEvent.Add(ID, waitTime);
				}
		// 开始等待
		stuff.waitStart();
}

	public int getRandomEventID() {
		int range = _postiveEventList.Count;
		int randomIndex = Random.Range(0, range - 1);
		return _postiveEventList[randomIndex];
	}

	private void showTriggerStuff(int ID) {
		BasicStuff stuff = stuffList.Find(x => x.ID == ID);
		 if (stuff.isOrder) stuff.gameObject.SetActive(true);
	}

	private void hidenTriggerStuff(int ID) {
		BasicStuff stuff = stuffList.Find(x => x.ID == ID);
		if (stuff.isOrder) stuff.gameObject.SetActive(false);
	}

	public void dispatchTheEventStuff (int ID) {
		_postiveEventList.Remove(ID);
		// 隐藏对象
		showTriggerStuff(ID);
	}

	public void fullBackTheEventStuff(int ID) {
		_postiveEventList.Add(ID);
		// 隐藏对象
		hidenTriggerStuff(ID);
	}

	// finished 的时候一定会执行
	public void eventClear(int ID) {
		_timer.removeSliderTimeRec(ID);
		if (_sliderDic[ID] != null) {
			_sliderDic[ID].gameObject.SetActive(false);
		}
		if (ID == _playerStuff.ID) _playerStuff.finishedEvent(_playerStuff.ID);
	}
}
