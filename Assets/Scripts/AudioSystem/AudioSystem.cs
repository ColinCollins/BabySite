using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour {

	public AudioClip cry1;
	public AudioClip telephone;
	public AudioSource audioCtrl;

	private static AudioSystem _instance = null;
	private void Awake()
	{
		_instance = this;

	}

	public static AudioSystem getInstance() {
		if (_instance == null) {
			Debug.LogWarning("AudioSystem has been lost");
			return null;
		}
		return _instance;
	}
	// 播放对应的特殊音效
	public void playCry1(bool isPlay) {
		playCtrl(cry1, isPlay);
	}

	public void playTelePhone(bool isPlay) {
		playCtrl(telephone, isPlay);
	}

	public void playCtrl(AudioClip clip, bool isPlay) {
		if (isPlay)
		{
			audioCtrl.clip = clip;
			audioCtrl.Play();
			audioCtrl.loop = true;
		}
		else
		{
			audioCtrl.Stop();
			audioCtrl.clip = null;
		}
	}
}
