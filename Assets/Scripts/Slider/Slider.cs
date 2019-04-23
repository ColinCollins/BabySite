using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slider : MonoBehaviour {
	private float _process = 0;
	public Transform bg;
	public Transform slider;

	public float value {
		get {
			return _process;
		}
		set {
			_process = value;
			slider.localScale = new Vector3(value, 1, 1);
		}
	}
}
