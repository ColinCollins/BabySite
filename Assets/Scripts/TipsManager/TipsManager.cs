using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour {

	public Text tips;

	private static TipsManager _instance = null;

	private void Awake()
	{
		_instance = this;
	}

	public static TipsManager getInstance() {
		if (_instance == null) {
			_instance = new TipsManager();
			_instance.tips = GameObject.Find("Text").GetComponent<Text>();
			return _instance;
		}
		return _instance;
	}

	public void setTips(string msg) {
		tips.text = msg;
	}


}
